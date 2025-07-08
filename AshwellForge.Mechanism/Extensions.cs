using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AshwellForge.Mechanism;

public static class Extensions
{
    public static IServiceCollection AddLiveStreamServer(this IServiceCollection services, ServerOptions options)
    {
        services.AddLiveStreamingServer(
            new IPEndPoint(IPAddress.Any, options.Port),
            conf =>
            {
                conf.Services.AddSingleton<RtmpStreamManagerApiService>();
                conf.AddFlv();
            });

        services.AddStreamOperations();
        return services;
    }

    public static IEndpointRouteBuilder MapStreamManagerEndpoints(this IEndpointRouteBuilder builder, string streamsBaseUri) =>
        builder.MapStreamManagerApiEndpoints(streamsBaseUri);
}
