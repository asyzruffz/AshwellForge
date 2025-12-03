namespace AshwellForge.Core.RtmpServer;

public interface ISessionControl : ISessionInfo
{
    void Disconnect();
    Task DisconnectAsync(CancellationToken cancellation = default(CancellationToken));
}
