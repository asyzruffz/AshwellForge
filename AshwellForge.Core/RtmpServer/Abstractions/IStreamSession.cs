namespace AshwellForge.Core.RtmpServer;

public interface IStreamSession
{
    StreamEndPoint? EndPoint { get; }

    Task RunAsync(StreamEndPoint streamEndPoint, CancellationToken cancellationToken);

    IReadOnlyList<ISessionHandle> Clients { get; }

    ISessionHandle? GetClient(uint clientId);
}
