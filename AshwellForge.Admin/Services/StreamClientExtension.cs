using Microsoft.Extensions.Options;

namespace AshwellForge.Admin.Services;

public static class StreamClientExtension
{
    public static IServiceCollection AddStreamingClient(this IServiceCollection services)
    {
        services.AddHttpClient<StreamsApi>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<AshwellForgeSettings>>().Value;

            client.BaseAddress = new Uri(settings.StreamsBaseAddress);
            //client.DefaultRequestHeaders.Add("Authorization", settings.AccessToken);
        });

        return services;
    }
}
