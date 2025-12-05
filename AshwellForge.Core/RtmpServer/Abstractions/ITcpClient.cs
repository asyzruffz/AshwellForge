using System.Net.Sockets;
using Stream = System.IO.Stream;

namespace AshwellForge.Core.RtmpServer;

public interface ITcpClient : IClientConnection, IDisposable
{
    void Close();
    Socket Client { get; }
    Stream GetStream();
}
