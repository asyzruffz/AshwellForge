namespace AshwellForge.Core.RtmpServer;

internal interface IServerEventDispatcher
{
    Task ListenerCreatedAsync(ITcpListener tcpListener);

    Task ClientAcceptedAsync(ITcpClient tcpClient);

    Task ClientConnectedAsync(ISessionHandle client);

    Task ClientDisconnectedAsync(ISessionInfo client);

    Task ServerStartedAsync();

    Task ServerStoppedAsync();
}
