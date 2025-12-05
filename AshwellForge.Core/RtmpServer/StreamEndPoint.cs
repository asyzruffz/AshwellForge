using System.Net;

namespace AshwellForge.Core.RtmpServer;

public record StreamEndPoint(IPEndPoint IPEndPoint, bool IsSecure)
{
    public static implicit operator StreamEndPoint(IPEndPoint ipEndPoint) =>
        new StreamEndPoint(ipEndPoint, IsSecure: false);

    public override string ToString() => IPEndPoint.ToString();
}
