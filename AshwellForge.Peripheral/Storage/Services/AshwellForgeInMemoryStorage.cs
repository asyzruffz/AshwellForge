using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;

namespace AshwellForge.Peripheral.Storage.Services;

internal class AshwellForgeInMemoryStorage : IAshwellForgeStorage
{
    readonly List<IngestServer> ingestServers = new();

    public Task<bool> HasIngestServers()
    {
        return Task.FromResult(ingestServers.Any());
    }

    public Task ClearIngestServers()
    {
        ingestServers.Clear();
        return Task.CompletedTask;
    }

    public Task SaveIngestServer(IngestServer server)
    {
        var existed = ingestServers
            .Where(s => s.Name == server.Name || s.Url == server.Url)
            .Select((s, i) => ingestServers[i] = server)
            .Any();
        if (!existed)
        {
            ingestServers.Add(server);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IngestServer>> GetIngestServers()
    {
        return Task.FromResult<IEnumerable<IngestServer>>(ingestServers);
    }
}
