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

    public async Task<ApiResult<TwitchIngests>> GetIngestServers()
    {
        var response = await client.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            return ApiResult<TwitchIngests>.Fail(new ApiError((int)response.StatusCode, "Fail connecting to Twitch"));
        }

        var content = await response.Content.ReadFromJsonAsync<TwitchIngests>();
        if (content is null)
        {
            return ApiResult<TwitchIngests>.Fail(ApiError.Internal("Fail json conversion to TwitchIngests"));
        }

        return ApiResult<TwitchIngests>.Ok(content!);
    }
}
