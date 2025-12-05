namespace AshwellForge.Core.RtmpServer;

public interface IClientSessionManager
{
    IReadOnlyList<ISessionHandle> GetClients();

    ISessionHandle? GetClient(uint clientId);

    Task AcceptClientAsync(ITcpListener tcpListener, StreamEndPoint serverEndPoint, CancellationToken cancellationToken);

    Task WaitUntilAllClientTasksCompleteAsync(CancellationToken cancellationToken = default(CancellationToken));
}
