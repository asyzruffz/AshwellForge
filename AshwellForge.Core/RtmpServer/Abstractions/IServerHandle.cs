namespace AshwellForge.Core.RtmpServer;

public interface IServerHandle
{
    bool IsStarted { get; }

    IReadOnlyList<ServerEndPoint>? EndPoints { get; }

    IReadOnlyList<ISessionHandle> Clients { get; }

    ISessionHandle? GetClient(uint clientId);
}
