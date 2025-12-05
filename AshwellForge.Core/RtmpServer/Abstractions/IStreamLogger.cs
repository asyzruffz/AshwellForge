using System.Net.Sockets;

namespace AshwellForge.Core.RtmpServer;

public interface IStreamLogger
{
    void StreamStarted();
    void StreamStarted(string ipEndPoint);
    void StreamStopped();
    void StreamShuttingDown();

    void StreamError(Exception ex);
    void AcceptClientError(SocketException exception);
    void StreamLoopError(Exception exception);

    void ClientConnected(uint clientId);
    void ClientDisconnected(uint clientId);
}
