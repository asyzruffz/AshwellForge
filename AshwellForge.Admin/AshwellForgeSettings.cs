namespace AshwellForge.Admin;

public class AshwellForgeSettings
{
    public const string SectionName = "AshwellForgeSettings";

    public string BaseAddress { get; set; } = string.Empty;
    public string RtmpBaseAddress { get; set; } = string.Empty;
    public string ApiBaseAddress { get; set; } = string.Empty;
    public bool HasHttpFlvPreview { get; set; }
    public string HttpFlvUriPattern { get; set; } = string.Empty;
    public bool HasHlsPreview { get; set; }
    public string HlsUriPattern { get; set; } = string.Empty;
}
