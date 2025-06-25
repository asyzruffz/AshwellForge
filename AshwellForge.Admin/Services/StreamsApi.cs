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
        var streams = new List<StreamEntry> { StreamEntry.Empty() };
        return streams;
    }
}
