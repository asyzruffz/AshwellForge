using System.Net.Sockets;

namespace AshwellForge.Core.RtmpServer;

public interface IServerLogger
{
    void ServerStarted();
    void ServerStarted(string ipEndPoint);
    void ServerStopped();
    void ServerShuttingDown();

    void ServerError(Exception ex);
    void AcceptClientError(SocketException exception);
    void ServerLoopError(Exception exception);

    void ClientConnected(uint clientId);
    void ClientDisconnected(uint clientId);

    void DispatchingListenerCreatedEventError(Exception ex);
    void DispatchingClientAcceptedEventError(Exception ex);
    void DispatchingClientConnectedEventError(Exception ex);
    void DispatchingClientDisconnectedEventError(Exception ex);
    void DispatchingServerStartedEventError(Exception ex);
    void DispatchingServerStoppedEventError(Exception ex);
}
