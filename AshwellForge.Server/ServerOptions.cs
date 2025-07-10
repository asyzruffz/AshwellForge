namespace AshwellForge.Server;

/// <summary>
/// Configuration options for the server.
/// </summary>
public class ServerOptions
{
    public const string SectionName = "ServerSettings";

    /// <summary>
    /// Port for the RTMP server endpoint. 
    /// Default: 1935.
    /// </summary>
    public int RtmpPort { get; set; } = 1935;
}
