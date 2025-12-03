using AshwellForge.Core.RtmpServer;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace AshwellForge.Mechanism.RtmpServer.Utils;

internal partial class ServerLogger : IServerLogger
{
    readonly ILogger logger;

    public ServerLogger(ILogger<ServerLogger> logger)
    {
        this.logger = logger;
    }

    [LoggerMessage(LogLevel.Information, "Server is started")]
    public partial void ServerStarted();

    [LoggerMessage(LogLevel.Information, "Server is started (IPEndPoint={IPEndPoint})")]
    public partial void ServerStarted(string ipEndPoint);

    [LoggerMessage(LogLevel.Information, "Server is stopped")]
    public partial void ServerStopped();

    [LoggerMessage(LogLevel.Information, "Server is shutting down")]
    public partial void ServerShuttingDown();

    [LoggerMessage(LogLevel.Error, "An error occurred while running the server")]
    public partial void ServerError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while accepting a client connection")]
    public partial void AcceptClientError(SocketException exception);

    [LoggerMessage(LogLevel.Error, "An error occurred in the server loop")]
    public partial void ServerLoopError(Exception exception);

    [LoggerMessage(LogLevel.Information, "Connected (ClientId={ClientId})")]
    public partial void ClientConnected(uint clientId);

    [LoggerMessage(LogLevel.Information, "Disconnected (ClientId={ClientId})")]
    public partial void ClientDisconnected(uint clientId);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching listener created event")]
    public partial void DispatchingListenerCreatedEventError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching client accepted event")]
    public partial void DispatchingClientAcceptedEventError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching client connected event")]
    public partial void DispatchingClientConnectedEventError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching client disconnected event")]
    public partial void DispatchingClientDisconnectedEventError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching server started event")]
    public partial void DispatchingServerStartedEventError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while dispatching server stopped event")]
    public partial void DispatchingServerStoppedEventError(Exception ex);
}
