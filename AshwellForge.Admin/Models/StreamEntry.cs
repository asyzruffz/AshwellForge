namespace AshwellForge.Admin.Models;

public record StreamEntry(
    string StreamPath,
    string Resolution,
    string Framerate,
    string VideoCodec,
    string AudioCodec,
    string SampleRate,
    string Channels,
    string Subscribers,
    string StartTime);
