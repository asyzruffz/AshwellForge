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
        Console.WriteLine($"URI: {client.BaseAddress?.ToString()}");

        var response = await client.GetAsync(requestParams.IncludeToUri("/api/v1/streams"));

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<StreamEntry>();
        }

        var streamsResponse = await response.Content.ReadFromJsonAsync<GetStreamsResponse>();
        if (streamsResponse is null)
        {
            return Enumerable.Empty<StreamEntry>();
        }

        /*await Task.Delay(600);
        var streams = new List<VideoStream>
        {
            new VideoStream
            {
                Id = "000",
                ClientId = 0,
                StreamPath = "unknown",
                StartTime = DateTime.Now,
                SubscribersCount = 0,
                StreamArguments = new Dictionary<string, string>(),
            }
        };*/

        return streamsResponse.Streams
            .Where(s => s is not null)
            .Select(stream => new StreamEntry(
            stream!.StreamPath,
            stream.StartTime.ToShortTimeString(),
            stream.SubscribersCount,
            $"{stream.Width}x{stream.Height}",
            stream.Framerate,
            stream.VideoCodecId.ToString(),
            stream.AudioCodecId.ToString(),
            stream.AudioSampleRate,
            stream.AudioChannels));
    }
}
