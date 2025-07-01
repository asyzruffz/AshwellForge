using AshwellForge.Mechanism.Admin;
using AshwellForge.Mechanism.Core;
using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.RtmpServer.Dtos;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AshwellForge.Mechanism;

public static class Extensions
{
    public static IServiceCollection AddLiveStreamServer(this IServiceCollection services, int port)
    {
        services.AddLiveStreamingServer(
            new IPEndPoint(IPAddress.Any, port),
            options =>
            {
                options.Services.AddSingleton<RtmpStreamManagerApiService>();
                options.AddFlv();
            });

        services.AddScoped<IOperationHandler<GetStreamsOperation, GetStreamsResponse>, GetStreamsOperationHandler>();
        services.AddScoped<IOperationHandler<DeleteStreamOperation>, DeleteStreamOperationHandler>();
        return services;
    }

    public static IApplicationBuilder UseFlv(this IApplicationBuilder app) => app.UseHttpFlv();

    public static IEndpointRouteBuilder MapServerApiEndpoints(this IEndpointRouteBuilder builder) =>
        builder.MapStreamManagerApiEndpoints();

    /// <summary>
    /// Adds the Admin Panel UI middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <param name="options">Optional configuration options for the Admin Panel UI.</param>
    /// <returns>The WebApplication instance for method chaining.</returns>
    public static WebApplication UseAdminPanelUI(this WebApplication app, AdminOptions options)
    {
        app.UseMiddleware<AdminMiddleware>(options);
        return app;
    }
}
