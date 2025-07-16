using AshwellForge.Core.Data;

namespace AshwellForge.Core.Abstractions;

public interface IAshwellForgeStorage
{
    Task<bool> HasTwitchServers();
    Task ClearIngestServers();
    Task SaveIngestServer(IngestServer server);
    Task SaveTwitchServer(IngestServer server);
    Task<IEnumerable<IngestServer>> GetSavedIngestServers();
    Task<IEnumerable<IngestServer>> GetTwitchIngestServers();
}
