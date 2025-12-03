using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.RtmpServer;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using AshwellForge.Mechanism.RtmpServer.Services;
using AshwellForge.Mechanism.RtmpServer.Utils;
using AshwellForge.Mechanism.Twitch.Operations;
using LiveStreamingServerNet.Flv.Installer;
using LiveStreamingServerNet.Rtmp.Server.Installer;
using LiveStreamingServerNet.Rtmp.Server.Installer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AshwellForge.Mechanism.RtmpServer;

internal static class Installer
{
    public static IServiceCollection AddLiveStreamingServer(this IServiceCollection services, int port)
    {
        IEnumerable<ServerEndPoint> serverEndPoints = [new IPEndPoint(IPAddress.Any, port)];

        services.AddSingleton<IServerLogger, ServerLogger>();

        services.AddRtmpServer(
            conf => conf
                .AddStreamManagerApi()
                .AddFlv(),
            null);

        services.AddHostedService((IServiceProvider svc) =>
            ActivatorUtilities.CreateInstance<LiveStreamServerRunner>(svc, [serverEndPoints.ToList()] ));
        return services;
    }

    static IRtmpServerConfigurator AddStreamManagerApi(this IRtmpServerConfigurator config)
    {
        config.Services.AddSingleton<IStreamManagerApiService, StreamManagerApiService>();
        return config;
    }

    public static IServiceCollection AddServerServices(this IServiceCollection services)
    {
        services.AddScoped<IIngestServerService, IngestServerService>();
        return services;
    }

    public static IServiceCollection AddServerOperations(this IServiceCollection services)
    {
        services.AddScoped<IApiOperationHandler<GetStreamsOperation, IEnumerable<VideoStream>>, GetStreamsOperationHandler>();
        services.AddScoped<IApiOperationHandler<DeleteStreamOperation>, DeleteStreamOperationHandler>();
        services.AddScoped<IApiOperationHandler<GetTwitchIngestServersOperation, IEnumerable<TwitchIngest>>, GetTwitchIngestServersOperationHandler>();
        services.AddScoped<IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>>, GetIngestServersOperationHandler>();
        services.AddScoped<IApiOperationHandler<SaveIngestServerOperation>, SaveIngestServerOperationHandler>();
        services.AddScoped<IApiOperationHandler<PublishStreamOperation>, PublishStreamOperationHandler>();
        return services;
    }
}
