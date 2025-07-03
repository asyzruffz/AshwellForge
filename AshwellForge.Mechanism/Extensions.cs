using AshwellForge.Mechanism.Admin;
using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.RtmpServer.Hls;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace AshwellForge.Mechanism;

public static class Extensions
{
    public static IServiceCollection AddLiveStreamServer(this IServiceCollection services, ServerOptions options)
    {
        services.AddLiveStreamingServer(
            new IPEndPoint(IPAddress.Any, options.Port),
            config =>
            {
                config.Services.AddSingleton<RtmpStreamManagerApiService>();
                config.AddFlv();
                config.AddHls();
            });

        services.AddStreamOperations();
        return services;
    }

    /// <summary>
    /// Adds the Admin Panel UI middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <param name="options">Optional configuration options for the Admin Panel UI.</param>
    /// <returns>The WebApplication instance for method chaining.</returns>
    public static WebApplication UseAdminPanelUI(this WebApplication app, AdminOptions options)
    {
        if (options.HasHttpFlvPreview)
        {
            app.UseHttpFlv();
        }
        if (options.HasHlsPreview)
        {
            app.UseHls();
        }

        app.MapStreamManagerApiEndpoints(options.StreamsBaseUri);
        app.UseMiddleware<AdminMiddleware>(options);
        return app;
    }
}
