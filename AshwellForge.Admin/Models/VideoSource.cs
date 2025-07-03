using BlazorVideoPlayer;

namespace AshwellForge.Admin.Models;

public record VideoSource(string Path, string Type)
{
    public static VideoSource Flv(string Path) => new VideoSource(Path, "video/x-flv");
    public static VideoSource Hls(string Path) => new VideoSource(Path, "application/x-mpegURL");
}

public static class VideoSourceExtension
{
    public static Source ToBVPSource(this VideoSource source) =>
        new Source { Src = source.Path, Type = source.Type };
}
