namespace AshwellForge.Core.RtmpServer;

public interface IStreamEventDispatcher
{
    Task StreamStartedAsync();
    Task StreamStoppedAsync();

    Task ListenerCreatedAsync(ITcpListener tcpListener);

    Task ClientAcceptedAsync(ITcpClient tcpClient);

    Task ClientConnectedAsync(ISessionHandle client);
    Task ClientDisconnectedAsync(ISessionInfo client);
}
