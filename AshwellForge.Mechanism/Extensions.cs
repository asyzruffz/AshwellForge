using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.Twitch;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AshwellForge.Mechanism;

public static class Extensions
{
    public static IServiceCollection AddLiveStreamServer(this IServiceCollection services, int port)
    {
        services.AddLiveStreamingServer(
            new IPEndPoint(IPAddress.Any, port),
            conf => conf
                .AddStreamManagerApi()
                .AddFlv());

        return services;
    }

    public static IServiceCollection AddMechanism(this IServiceCollection services)
    {
        services.AddServerServices();
        services.AddServerOperations();

        services.AddTwitchServices();
        services.AddTwitchOperations();
        return services;
    }
}
