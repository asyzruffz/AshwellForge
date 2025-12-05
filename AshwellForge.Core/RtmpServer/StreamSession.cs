using System.Net;
using System.Net.Sockets;

namespace AshwellForge.Core.RtmpServer;

public class StreamSession : IStreamSession
{
    readonly IClientSessionManager clientSessionManager;
    readonly IStreamEventDispatcher eventDispatcher;
    readonly IStreamLogger logger;

    public StreamEndPoint? EndPoint { get; private set; }

    public IReadOnlyList<ISessionHandle> Clients => clientSessionManager.GetClients();

    int isStarted;

    public StreamSession(IClientSessionManager clientSessionManager, IStreamEventDispatcher streamEventDispatcher, IStreamLogger streamLogger)
    {
        this.clientSessionManager = clientSessionManager;
        eventDispatcher = streamEventDispatcher;
        logger = streamLogger;
    }

    public async Task RunAsync(StreamEndPoint streamEndPoint, CancellationToken cancellationToken)
    {
        ValidateAndSetStarted();

        EndPoint = streamEndPoint;
        ServerListener? streamListener = null;
        try
        {
            streamListener = await StartStreamListener(streamEndPoint).ConfigureAwait(false);
            await OnStreamStartedAsync().ConfigureAwait(false);
            await RunStreamLoopAsync(streamListener, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.StreamShuttingDown();
        }
        catch (Exception exc)
        {
            logger.StreamError(exc);
        }

        if (streamListener is null) return; 

        await WaitUntilAllClientTasksCompleteAsync().ConfigureAwait(false);
        streamListener.Stop();
        await OnStreamStoppedAsync().ConfigureAwait(false);
    }

    void ValidateAndSetStarted()
    {
        if (Interlocked.CompareExchange(ref isStarted, 1, 0) == 1)
        {
            throw new InvalidOperationException("The stream session can only be started once.");
        }
    }

    async Task<ServerListener> StartStreamListener(StreamEndPoint streamEndPoint)
    {
        var streamListener = await CreateStreamListenerAsync(streamEndPoint.IPEndPoint);

        streamListener.Start();
        logger.StreamStarted(streamEndPoint.ToString());

        return streamListener;
    }

    async Task RunStreamLoopAsync(ServerListener streamListener, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await clientSessionManager.AcceptClientAsync(streamListener, streamListener.ServerEndPoint, cancellationToken);
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
                logger.StreamLoopError(exception2);
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

    async Task<ServerListener> CreateStreamListenerAsync(IPEndPoint localEndPoint)
    {
        ServerListener listener = new ServerListener(new TcpListener(localEndPoint), localEndPoint);
        await OnListenerCreatedAsync(listener).ConfigureAwait(false);
        return listener;
    }

    async Task OnListenerCreatedAsync(ITcpListener tcpListener)
    {
        await eventDispatcher.ListenerCreatedAsync(tcpListener);
    }

    async Task OnStreamStartedAsync()
    {
        await eventDispatcher.StreamStartedAsync();
        logger.StreamStarted();
    }

    async Task OnStreamStoppedAsync()
    {
        await eventDispatcher.StreamStoppedAsync();
        logger.StreamStopped();
    }

    public ISessionHandle? GetClient(uint clientId)
    {
        return clientSessionManager.GetClient(clientId);
    }
}
