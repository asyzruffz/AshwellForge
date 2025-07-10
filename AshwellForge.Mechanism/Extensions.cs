using AshwellForge.Core.Data;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.RtmpServer.Operations;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
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

    static IServiceCollection AddStreamOperations(this IServiceCollection services)
    {
        services.AddScoped<IOperationHandler<GetStreamsOperation, GetStreamsResponse>, GetStreamsOperationHandler>();
        services.AddScoped<IOperationHandler<DeleteStreamOperation>, DeleteStreamOperationHandler>();
        return services;
    }
}
