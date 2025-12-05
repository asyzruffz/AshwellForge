using AshwellForge.Core.RtmpServer;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace AshwellForge.Mechanism.RtmpServer.Utils;

internal partial class StreamLogger : IStreamLogger
{
    readonly ILogger logger;

    public StreamLogger(ILogger<StreamLogger> logger)
    {
        this.logger = logger;
    }

    [LoggerMessage(LogLevel.Information, "Stream is started")]
    public partial void StreamStarted();

    [LoggerMessage(LogLevel.Information, "Stream is started (IPEndPoint={IPEndPoint})")]
    public partial void StreamStarted(string ipEndPoint);

    [LoggerMessage(LogLevel.Information, "Stream is stopped")]
    public partial void StreamStopped();

    [LoggerMessage(LogLevel.Information, "Stream is shutting down")]
    public partial void StreamShuttingDown();

    [LoggerMessage(LogLevel.Error, "An error occurred while running the stream")]
    public partial void StreamError(Exception ex);

    [LoggerMessage(LogLevel.Error, "An error occurred while accepting a client connection")]
    public partial void AcceptClientError(SocketException exception);

    [LoggerMessage(LogLevel.Error, "An error occurred in the stream loop")]
    public partial void StreamLoopError(Exception exception);

    [LoggerMessage(LogLevel.Information, "Connected (ClientId={ClientId})")]
    public partial void ClientConnected(uint clientId);

    [LoggerMessage(LogLevel.Information, "Disconnected (ClientId={ClientId})")]
    public partial void ClientDisconnected(uint clientId);
}
