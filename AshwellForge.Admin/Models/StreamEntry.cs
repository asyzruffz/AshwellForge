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
        "/empty/none", 0, "00:00", 0, "0x0", 0,
        "Unknown", "Unknown", 0, 0);

    public static StreamEntry From(VideoStream stream) => new StreamEntry(
        stream!.StreamPath,
        (int)stream.ClientId,
        stream.StartTime.ToLocalTime().ToShortTimeString(),
        stream.SubscribersCount,
        $"{stream.Width}x{stream.Height}",
        stream.Framerate,
        ((VideoCodec)stream.VideoCodecId).ToName(),
        ((AudioCodec)stream.AudioCodecId).ToName(),
        stream.AudioSampleRate,
        stream.AudioChannels);
}
