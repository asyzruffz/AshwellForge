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

    public async Task<ApiResult<IEnumerable<TwitchIngest>>> GetIngestServers()
    {
        var response = await client.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            return ApiResult<IEnumerable<TwitchIngest>>.Fail(new ApiError((int)response.StatusCode, "Fail connecting to Twitch"));
        }

        var content = await response.Content.ReadFromJsonAsync<TwitchIngests>();
        if (content is null)
        {
            return ApiResult<IEnumerable<TwitchIngest>>.Fail(ApiError.Internal("Fail json conversion to TwitchIngests"));
        }

        return ApiResult<IEnumerable<TwitchIngest>>.Ok(content!.Ingests);
    }
}
