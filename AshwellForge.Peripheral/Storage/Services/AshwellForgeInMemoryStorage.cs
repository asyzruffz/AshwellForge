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
            .Select((s, i) => (s, i))
            .Where(e => e.s.Name == server.Name || e.s.Url == server.Url)
            .Select(s => ingestServers[s.i] = server)
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
