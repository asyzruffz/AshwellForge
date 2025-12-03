using System.Net;
using System.Net.Sockets;

namespace AshwellForge.Core.RtmpServer;

internal sealed class Server : IServer, IServerHandle
{
    readonly IClientSessionManager clientSessionManager;

    readonly IServerEventDispatcher eventDispatcher;

    readonly IServerLogger logger;

    public IServiceProvider Services { get; }

    public bool IsStarted => isStarted == 1;
    int isStarted;

    public IReadOnlyList<ISessionHandle> Clients => clientSessionManager.GetClients();

    public IReadOnlyList<ServerEndPoint>? EndPoints { get; private set; }

    public Server(IServiceProvider services, IClientSessionManager clientSessionManager, IServerEventDispatcher eventDispatcher, IServerLogger logger)
    {
        Services = services;
        this.clientSessionManager = clientSessionManager;
        this.eventDispatcher = eventDispatcher;
        this.logger = logger;
    }

    public Task RunAsync(ServerEndPoint serverEndPoint, CancellationToken cancellationToken = default)
    {
        return RunAsync(new List<ServerEndPoint> { serverEndPoint }, cancellationToken);
    }

    public async Task RunAsync(IReadOnlyList<ServerEndPoint> serverEndPoints, CancellationToken cancellationToken = default)
    {
        ValidateAndSetStarted();

        Exception? serverException = null;
        EndPoints = new List<ServerEndPoint>(serverEndPoints).AsReadOnly();
        List<ServerListener> serverListeners = new List<ServerListener>();
        try
        {
            serverListeners = await StartServerListeners(new List<ServerEndPoint>(serverEndPoints)).ConfigureAwait(false);
            await OnServerStartedAsync().ConfigureAwait(false);
            await RunServerLoopsAsync(serverListeners, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.ServerShuttingDown();
        }
        catch (Exception exc)
        {
            logger.ServerError(exc);
            serverException = exc;
        }

        await WaitUntilAllClientTasksCompleteAsync().ConfigureAwait(false);
        StopAllListeners(serverListeners);
        await OnServerStoppedAsync().ConfigureAwait(false);

        if (serverException != null)
        {
            throw serverException;
        }
    }

    void ValidateAndSetStarted()
    {
        if (Interlocked.CompareExchange(ref isStarted, 1, 0) == 1)
        {
            throw new InvalidOperationException("The server can only be started once.");
        }
    }

    async Task<List<ServerListener>> StartServerListeners(IReadOnlyList<ServerEndPoint> serverEndPoints)
    {
        List<ServerListener> serverListeners = new List<ServerListener>();
        foreach (ServerEndPoint serverEndPoint in serverEndPoints)
        {
            var serverListener = await CreateServerListenerAsync(serverEndPoint.IPEndPoint);
            serverListeners.Add(serverListener);
            serverListener.Start();
            logger.ServerStarted(serverEndPoint.ToString());
        }

        return serverListeners;
    }

    async Task RunServerLoopsAsync(IEnumerable<ServerListener> serverListeners, CancellationToken cancellationToken)
    {
        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        try
        {
            await Task.WhenAll(serverListeners.Select(listener => RunServerLoopAsync(listener, cts)));
        }
        finally
        {
            if (cts != null)
            {
                ((IDisposable)cts).Dispose();
            }
        }
    }

    async Task RunServerLoopAsync(ServerListener serverListener, CancellationTokenSource cts)
    {
        CancellationToken cancellationToken = cts.Token;
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await clientSessionManager.AcceptClientAsync(serverListener, serverListener.ServerEndPoint, cancellationToken);
            }
            catch (SocketException exception)
            {
                logger.AcceptClientError(exception);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception2)
            {
                logger.ServerLoopError(exception2);
                cts.Cancel();
                throw;
            }
        }
    }

    async Task WaitUntilAllClientTasksCompleteAsync()
    {
        try
        {
            await clientSessionManager.WaitUntilAllClientTasksCompleteAsync();
        }
        catch { }
    }

    void StopAllListeners(IEnumerable<ServerListener> serverListeners)
    {
        foreach (ServerListener serverListener in serverListeners)
        {
            serverListener.Stop();
        }
    }

    async Task<ServerListener> CreateServerListenerAsync(IPEndPoint localEndPoint)
    {
        ServerListener listener = new ServerListener(new TcpListener(localEndPoint), localEndPoint);
        await OnListenerCreatedAsync(listener).ConfigureAwait(false);
        return listener;
    }

    async Task OnListenerCreatedAsync(ITcpListener tcpListener)
    {
        await eventDispatcher.ListenerCreatedAsync(tcpListener);
    }

    async Task OnServerStartedAsync()
    {
        await eventDispatcher.ServerStartedAsync();
        logger.ServerStarted();
    }

    async Task OnServerStoppedAsync()
    {
        await eventDispatcher.ServerStoppedAsync();
        logger.ServerStopped();
    }

    public ISessionHandle? GetClient(uint clientId)
    {
        return clientSessionManager.GetClient(clientId);
    }
}
