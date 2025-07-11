using AshwellForge.Core.Abstractions;
using AshwellForge.Peripheral.Storage.Services;
using AshwellForge.Peripheral.Twitch.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Peripheral;

public static class Extensions
{
    public static IServiceCollection AddPeripheral(this IServiceCollection services)
    {
        services.AddSingleton<IAshwellForgeStorage, AshwellForgeInMemoryStorage>();

        services.AddHttpClient<TwitchIngestService>();
        services.AddScoped<ITwitchIngestService, TwitchIngestService>();
        return services;
    }
}
