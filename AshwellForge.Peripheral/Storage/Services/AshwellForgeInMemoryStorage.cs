using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;

namespace AshwellForge.Peripheral.Storage.Services;

internal class AshwellForgeInMemoryStorage : IAshwellForgeStorage
{
    readonly List<IngestServer> savedServers = new();
    readonly List<IngestServer> twitchServers = new();

    public Task<bool> HasTwitchServers()
    {
        return Task.FromResult(twitchServers.Any());
    }

    public Task ClearIngestServers()
    {
        savedServers.Clear();
        return Task.CompletedTask;
    }

    public Task SaveIngestServer(IngestServer server)
    {
        savedServers.Add(server);
        return Task.CompletedTask;
    }

    public Task SaveTwitchServer(IngestServer server)
    {
        var existed = twitchServers
            .Select((s, i) => (s, i))
            .Where(e => e.s.Name == server.Name || e.s.Url == server.Url)
            .Select(s => twitchServers[s.i] = server)
            .Any();
        if (!existed)
        {
            twitchServers.Add(server);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IngestServer>> GetSavedIngestServers() =>
        Task.FromResult<IEnumerable<IngestServer>>(savedServers);

    public Task<IEnumerable<IngestServer>> GetTwitchIngestServers() =>
        Task.FromResult<IEnumerable<IngestServer>>(twitchServers);
}
