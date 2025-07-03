using BlazorVideoPlayer;
using System;

namespace AshwellForge.Admin.Models;

public record VideoSource(string Path, string Type)
{
    public static VideoSource Flv(string Path) => new VideoSource(Path, "video/x-flv");
}

public static class VideoSourceExtension
{
    public static Source ToBVPSource(this VideoSource source) =>
        new Source { Src = source.Path, Type = source.Type };
}
