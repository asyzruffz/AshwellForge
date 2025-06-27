namespace AshwellForge.Admin.Models;

public record StreamEntry(
    string StreamPath,
    int ClientId,
    string StartTime,
    int Subscribers,
    string Resolution,
    int Framerate,
    string VideoCodec,
    string AudioCodec,
    int SampleRate,
    int Channels)
{
    public static StreamEntry Empty() => new StreamEntry(
        "...", 0, string.Empty, 0, "0x0", 0,
        string.Empty, string.Empty, 0, 0);

    public static StreamEntry From(VideoStream stream) => new StreamEntry(
        stream!.StreamPath,
        (int)stream.ClientId,
        stream.StartTime.ToShortTimeString(),
        stream.SubscribersCount,
        $"{stream.Width}x{stream.Height}",
        stream.Framerate,
        stream.VideoCodecId.ToString(),
        stream.AudioCodecId.ToString(),
        stream.AudioSampleRate,
        stream.AudioChannels);
}
