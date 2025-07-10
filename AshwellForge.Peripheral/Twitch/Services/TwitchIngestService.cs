using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using System.Net.Http.Json;

namespace AshwellForge.Peripheral.Twitch.Services;

public class TwitchIngestService : ITwitchIngestService
{
    readonly HttpClient client;

    public TwitchIngestService(HttpClient httpClient)
    {
        client = httpClient;
        client.BaseAddress = new Uri("https://ingest.twitch.tv/ingests");
    }

    public async Task<Result<TwitchIngests>> GetIngestServers()
    {
        var response = await client.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            return Result<TwitchIngests>.Fail($"{(int)response.StatusCode}:{response.StatusCode}");
        }

        var content = await response.Content.ReadFromJsonAsync<TwitchIngests>();
        return Result<TwitchIngests>.Ok(content);
    }
}
