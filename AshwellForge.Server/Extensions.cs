using AshwellForge.Mechanism;

namespace AshwellForge.Server;

public static class Extensions
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(ServerOptions.SectionName)
            .Get<ServerOptions>() ?? new ServerOptions();
        return services.AddLiveStreamServer(settings.RtmpPort);
    }
}
