using AshwellForge.Admin.Models;
using AshwellForge.Admin.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AshwellForge.Admin.Services;

public class StreamsApi
{
    readonly HttpClient client;
    readonly string apiBase;

    public StreamsApi(HttpClient httpClient, IOptions<AshwellForgeSettings> options)
    {
        client = httpClient;
        client.BaseAddress = new Uri(options.Value.BaseAddress);
        apiBase = options.Value.ApiBaseAddress;
    }

    public async Task<IEnumerable<StreamEntry>> GetStreams()
    {
        var requestParams = new GetStreamsParam(1, 20, null);

        var response = await client.GetAsync($"{apiBase}/streams{requestParams.ForUri()}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Fail to get streams");
            return Enumerable.Empty<StreamEntry>();
        }

        var streamsResponse = await response.Content.ReadFromJsonAsync<GetStreamsResponse>();
        if (streamsResponse is null)
        {
            Console.WriteLine("GetStreamsResponse cannot be made from json");
            return Enumerable.Empty<StreamEntry>();
        }

        return streamsResponse.Streams
            .Select(StreamEntry.From);
    }

    public async Task<IEnumerable<IngestServer>> GetIngestServers(bool forceRefresh = false)
    {
        var requestParams = UrlQueryString.Create()
            .Set("refresh", forceRefresh.ToString())
            .Build();

        var response = await client.GetAsync($"{apiBase}/ingests{requestParams}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Fail to get ingest servers");
            return Enumerable.Empty<IngestServer>();
        }

        var ingestsResponse = await response.Content.ReadFromJsonAsync<IEnumerable<IngestServer>>();
        if (ingestsResponse is null)
        {
            Console.WriteLine("IEnumerable<IngestServer> cannot be made from json");
            return Enumerable.Empty<IngestServer>();
        }

        return ingestsResponse;
    }
}
