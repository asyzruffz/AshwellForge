using AshwellForge.Core.Data;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.Twitch.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.Twitch;

internal static class Installer
{
    public static IServiceCollection AddTwitchServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddTwitchOperations(this IServiceCollection services)
    {
        services.AddScoped<IApiOperationHandler<GetTwitchIngestServersOperation, IEnumerable<TwitchIngest>>, GetTwitchIngestServersOperationHandler>();
        return services;
    }
}
