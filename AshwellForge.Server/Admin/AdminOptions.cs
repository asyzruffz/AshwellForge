namespace AshwellForge.Server.Admin;

/// <summary>
/// Configuration options for the Admin Panel UI.
/// </summary>
public class AdminOptions
{
    /// <summary>
    /// Base path for the admin panel UI. 
    /// Default: "/ui".
    /// </summary>
    public string BasePath { get; set; } = "/ui";

    /// <summary>
    /// Base URI for stream-related API endpoints. 
    /// Default: "/api/v1/streams".
    /// </summary>
    public string StreamsBaseUri { get; set; } = "/api/v1/streams";

    /// <summary>
    /// Indicates whether HTTP FLV preview functionality is enabled.
    /// </summary>
    public bool HasHttpFlvPreview { get; set; }

    /// <summary>
    /// URI pattern for HTTP FLV streams. 
    /// Default: "{streamPath}.flv".
    /// </summary>
    public string HttpFlvUriPattern { get; set; } = "{streamPath}.flv";

    /// <summary>
    /// Indicates whether HLS preview functionality is enabled.
    /// </summary>
    public bool HasHlsPreview { get; set; }

    /// <summary>
    /// URI pattern for HLS streams.
    /// Default: "{streamPath}/output.m3u8".
    /// </summary>
    public string HlsUriPattern { get; set; } = "{streamPath}/output.m3u8";
}
