namespace AshwellForge.Core.RtmpServer;

public interface IStreamSessionService
{
    Task<Guid> SpawnStreamSession();
    Task StopStreamSession(Guid id);
}
