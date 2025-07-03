using AshwellForge.Mechanism.Admin;
using AshwellForge.Mechanism.RtmpServer;
using AshwellForge.Mechanism.RtmpServer.Dtos;
using AshwellForge.Mechanism.RtmpServer.Hls;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet;
using LiveStreamingServerNet.Flv.Installer;
using LiveStreamingServerNet.Rtmp;
using LiveStreamingServerNet.StreamProcessor.AspNetCore.Installer;
using LiveStreamingServerNet.StreamProcessor.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

                var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
                new DirectoryInfo(outputDir).Create();
                conf.Configure(options => options.EnableGopCaching = false)
                    .AddVideoCodecFilter(builder => builder.Include(VideoCodec.AVC).Include(VideoCodec.HEVC))
                    .AddAudioCodecFilter(builder => builder.Include(AudioCodec.AAC))
                    .AddStreamProcessor(options =>
                        options.AddStreamProcessorEventHandler(svc =>
                            new StreamProcessorEventListener(outputDir, svc.GetRequiredService<ILogger<StreamProcessorEventListener>>())))
                    .AddHlsTransmuxer(options => options.Configure(config => config.OutputPathResolver = new HlsOutputPathResolver(outputDir)));
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
            app.UseHlsFiles();
        }

        app.MapStreamManagerApiEndpoints(options.StreamsBaseUri);
        app.UseMiddleware<AdminMiddleware>(options);
        return app;
    }
}
