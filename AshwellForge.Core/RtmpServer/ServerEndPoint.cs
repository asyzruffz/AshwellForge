using System.Net;

namespace AshwellForge.Core.RtmpServer;

public record ServerEndPoint(IPEndPoint IPEndPoint, bool IsSecure)
{
    public static implicit operator ServerEndPoint(IPEndPoint ipEndPoint) =>
        new ServerEndPoint(ipEndPoint, IsSecure: false);

    public override string ToString() => IPEndPoint.ToString();
}
