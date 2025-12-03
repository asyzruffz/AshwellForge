using AshwellForge.Core.RtmpServer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AshwellForge.Mechanism.RtmpServer.Services;

internal class LiveStreamServerRunner : BackgroundService
{
    readonly IServer server;

    readonly ILogger logger;

    readonly IReadOnlyList<ServerEndPoint> serverEndPoints;

    public LiveStreamServerRunner(IServer server, ILogger<LiveStreamServerRunner> logger, IReadOnlyList<ServerEndPoint> serverEndPoints)
    {
        this.server = server;
        this.logger = logger;
        this.serverEndPoints = serverEndPoints;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await server.RunAsync(serverEndPoints, stoppingToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while running the live streaming server");
            throw;
        }
    }
}
