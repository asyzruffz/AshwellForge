using AshwellForge.Admin.Models;

namespace AshwellForge.Admin.Services;

public class StreamsApi
{
    readonly HttpClient client;

    public StreamsApi(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<StreamEntry>> GetStreams()
    {
        await Task.Delay(600);
        var streams = new List<VideoStream> { new VideoStream() };

        return streams.Select(stream => new StreamEntry(
            stream.StreamPath,
            stream.StartTime,
            stream.SubscribersCount,
            $"{stream.Width}x{stream.Height}",
            stream.Framerate,
            stream.VideoCodecId.ToString(),
            stream.AudioCodecId.ToString(),
            stream.AudioSampleRate,
            stream.AudioChannels));
    }
}
