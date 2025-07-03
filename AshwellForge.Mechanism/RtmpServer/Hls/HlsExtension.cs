using LiveStreamingServerNet.Rtmp;
using LiveStreamingServerNet.Rtmp.Server.Installer.Contracts;
using LiveStreamingServerNet.StreamProcessor.AspNetCore.Installer;
using LiveStreamingServerNet.StreamProcessor.Installer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AshwellForge.Mechanism.RtmpServer.Hls;

internal static class HlsExtension
{
    public static IRtmpServerConfigurator AddHls(this IRtmpServerConfigurator config)
    {
        var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        new DirectoryInfo(outputDir).Create();

        config.Configure(options => options.EnableGopCaching = false)
            .AddVideoCodecFilter(builder => builder.Include(VideoCodec.AVC).Include(VideoCodec.HEVC))
            .AddAudioCodecFilter(builder => builder.Include(AudioCodec.AAC))
            .AddStreamProcessor(options =>
                options.AddStreamProcessorEventHandler(svc =>
                    new StreamProcessorEventListener(outputDir, svc.GetRequiredService<ILogger<StreamProcessorEventListener>>())))
            .AddHlsTransmuxer(options => options.Configure(config => config.OutputPathResolver = new HlsOutputPathResolver(outputDir)));
        return config;
    }

    public static IApplicationBuilder UseHls(this IApplicationBuilder app) => app.UseHlsFiles();
}
