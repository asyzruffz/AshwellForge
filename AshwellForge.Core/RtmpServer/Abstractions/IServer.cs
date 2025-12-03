namespace AshwellForge.Core.RtmpServer;

public interface IServer : IServerHandle
{
    IServiceProvider Services { get; }

    Task RunAsync(ServerEndPoint serverEndPoint, CancellationToken cancellationToken = default);

    Task RunAsync(IReadOnlyList<ServerEndPoint> serverEndPoints, CancellationToken cancellationToken = default);
}
