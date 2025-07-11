namespace AshwellForge.Core.Data;

public record IngestServer(string Name, string Url, string UrlSecure)
{
    public static IngestServer FromTwitch(TwitchIngest ingest) =>
        new IngestServer(
            $"Twitch - {ingest.Name}",
            ingest.UrlTemplate.Replace("stream_key", "0"),
            ingest.UrlTemplate.Replace("stream_key", "0"));
}
