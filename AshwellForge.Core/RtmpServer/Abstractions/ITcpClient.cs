using System.Net.Sockets;
using Stream = System.IO.Stream;

namespace AshwellForge.Core.RtmpServer;

internal interface ITcpClient : IClientConnection, IDisposable
{
    void Close();
    Socket Client { get; }
    Stream GetStream();
}
