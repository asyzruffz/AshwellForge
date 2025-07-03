namespace AshwellForge.Mechanism.RtmpServer;

/// <summary>
/// Configuration options for the RTMP server.
/// </summary>
public class ServerOptions
{
    /// <summary>
    /// Port for the server endpoint. 
    /// Default: 1935.
    /// </summary>
    public int Port { get; set; } = 1935;
}
