namespace AshwellForge.Core.RtmpServer;

internal interface IClientSessionManager
{
    IReadOnlyList<ISessionHandle> GetClients();

    ISessionHandle? GetClient(uint clientId);

    Task AcceptClientAsync(ITcpListener tcpListener, ServerEndPoint serverEndPoint, CancellationToken cancellationToken);

    Task WaitUntilAllClientTasksCompleteAsync(CancellationToken cancellationToken = default(CancellationToken));
}
