﻿using AshwellForge.Mechanism.Abstractions;
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
        services.AddSender();

        services.AddServerServices();
        services.AddServerOperations();

        services.AddTwitchServices();
        services.AddTwitchOperations();
        return services;
    }

    static IServiceCollection AddSender(this IServiceCollection services)
    {
        services.AddScoped<ISender, Sender>(sp => new Sender(sp));
        services.AddScoped<IApiSender, ApiSender>(sp => new ApiSender(sp));
        return services;
    }
}
