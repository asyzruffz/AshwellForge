namespace AshwellForge.Core.RtmpServer;

public interface IServerEventDispatcher
{
    Task ServerStartedAsync();
    Task ServerStoppedAsync();

    Task StreamSessionStartedAsync(Guid id);
    Task StreamSessionStoppedAsync(Guid id);
}
