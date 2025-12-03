using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace AshwellForge.Core.RtmpServer;

internal record ServerListener(TcpListener TcpListener, ServerEndPoint ServerEndPoint) : ITcpListener
{
    public EndPoint LocalEndpoint => TcpListener.LocalEndpoint;

    public void Start() => TcpListener.Start();
    public void Stop() => TcpListener.Stop();
    public bool Pending() => TcpListener.Pending();

    public async ValueTask<ITcpClient> AcceptTcpClientAsync(NetworkConfiguration config, CancellationToken cancellationToken)
    {
        var tcpClient = await TcpListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

        if (config.PreferInlineCompletionsOnNonWindows && Environment.OSVersion.Platform != PlatformID.Win32NT)
        {
            typeof(Socket).GetProperty("PreferInlineCompletions", BindingFlags.NonPublic | BindingFlags.Instance)?
                .SetValue(tcpClient.Client, true);
        }

        tcpClient.ReceiveBufferSize = config.ReceiveBufferSize;
        tcpClient.SendBufferSize = config.SendBufferSize;
        tcpClient.NoDelay = config.NoDelay;

        return new ClientWrapper(tcpClient);
    }
}
