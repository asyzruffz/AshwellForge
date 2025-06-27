using AshwellForge.Admin.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AshwellForge.Admin.Services;

public class StreamsApi
{
    readonly HttpClient client;

    public StreamsApi(HttpClient httpClient, IOptions<AshwellForgeSettings> options)
    {
        client = httpClient;
        client.BaseAddress = new Uri(options.Value.StreamsBaseAddress);
    }

    public async Task<IEnumerable<StreamEntry>> GetStreams()
    {
        var requestParams = new GetStreamsParam(1, 20, null);

        var response = await client.PerformRequest(requestParams.AppendToUri("/api/v1/streams"));

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Fail to get streams");
            return [StreamEntry.Empty()];
            //return Enumerable.Empty<StreamEntry>();
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
}
