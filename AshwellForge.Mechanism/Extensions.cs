using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using AshwellForge.Mechanism.RtmpServer.Services;
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
            conf =>
            {
                conf.Services.AddSingleton<IRtmpStreamManagerApiService, RtmpStreamManagerApiService>();
                conf.AddFlv();
            });

        return services;
    }

    public static IServiceCollection AddMechanism(this IServiceCollection services)
    {
        services.AddScoped<IIngestServersService, IngestServersService>();
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
