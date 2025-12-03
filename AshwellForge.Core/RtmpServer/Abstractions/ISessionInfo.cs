using System.Net;

namespace AshwellForge.Core.RtmpServer;

public interface ISessionInfo
{
    uint Id { get; }
    bool IsConnected { get; }
    DateTime StartTime { get; }
    EndPoint LocalEndPoint { get; }
    EndPoint RemoteEndPoint { get; }
}
