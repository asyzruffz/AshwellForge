using AshwellForge.Core.Data;

namespace AshwellForge.Core.Abstractions;

public interface IAshwellForgeStorage
{
    Task<bool> HasIngestServers();
    Task ClearIngestServers();
    Task SaveIngestServer(IngestServer server);
    Task<IEnumerable<IngestServer>> GetIngestServers();
}
