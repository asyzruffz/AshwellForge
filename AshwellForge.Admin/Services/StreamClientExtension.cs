namespace AshwellForge.Admin.Services;

public static class StreamClientExtension
{
    public static IServiceCollection AddStreamingClient(this IServiceCollection services)
    {
        services.AddHttpClient<StreamsApi>();
        services.AddScoped<StreamsApi>();
        return services;
    }
}
