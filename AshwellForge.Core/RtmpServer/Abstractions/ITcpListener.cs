using System.Net;

namespace AshwellForge.Core.RtmpServer;

public interface ITcpListener
{
    EndPoint LocalEndpoint { get; }

    bool Pending();

    void Start();

    void Stop();

    ValueTask<ITcpClient> AcceptTcpClientAsync(NetworkConfiguration config, CancellationToken cancellationToken);
}
