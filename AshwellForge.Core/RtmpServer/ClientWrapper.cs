using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

namespace AshwellForge.Core.RtmpServer;

internal class ClientWrapper : ITcpClient
{
    readonly TcpClient tcpClient;

    public ClientWrapper(TcpClient tcpClient)
    {
        this.tcpClient = tcpClient;
    }

    public int Available => tcpClient.Available;
    public bool Connected => tcpClient.Connected;
    public Socket Client => tcpClient.Client;
    public int ReceiveBufferSize { get => tcpClient.ReceiveBufferSize; set => tcpClient.ReceiveBufferSize = value; }
    public int SendBufferSize { get => tcpClient.SendBufferSize; set => tcpClient.SendBufferSize = value; }
    public int ReceiveTimeout { get => tcpClient.ReceiveTimeout; set => tcpClient.ReceiveTimeout = value; }
    public int SendTimeout { get => tcpClient.SendTimeout; set => tcpClient.SendTimeout = value; }
    public bool NoDelay { get => tcpClient.NoDelay; set => tcpClient.NoDelay = value; }
    [DisallowNull] public LingerOption? LingerState { get => tcpClient.LingerState; set => tcpClient.LingerState = value; }

    public Stream GetStream() => tcpClient.GetStream();
    public void Close() => tcpClient.Close();
    public void Dispose() => tcpClient.Dispose();
}
