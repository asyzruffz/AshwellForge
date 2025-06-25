namespace AshwellForge.Admin.Models;

public record StreamEntry(
    string StreamPath,
    string StartTime,
    int Subscribers,
    string Resolution,
    int Framerate,
    string VideoCodec,
    string AudioCodec,
    int SampleRate,
    int Channels)
{
    public static StreamEntry Empty() => new StreamEntry(string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, string.Empty, 0, 0);
}
