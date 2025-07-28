using AshwellForge.Delivery;
using LiveStreamingServerNet.Flv.Installer;

namespace AshwellForge.Server.Admin;

internal static class AdminExtension
{
    public static IServiceCollection AddAdmin(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();
        services.AddEndpoints();
        return services;
    }

    /// <summary>
    /// Adds the Admin UI to the application.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <param name="options">Optional configuration options for the Admin Panel UI.</param>
    /// <returns>The WebApplication instance for method chaining.</returns>
    public static WebApplication UseAdmin(this WebApplication app, AdminOptions options)
    {
        if (options.HasHttpFlvPreview)
        {
            app.UseHttpFlv();
        }

        app.MapAdminApiEndpoints(options.ApiBaseUri);
        app.MapAdminComponents();
        return app;
    }

    static IEndpointRouteBuilder MapAdminComponents(this IEndpointRouteBuilder app)
    {
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(AshwellForge.Admin.AssemblyReference.Assembly);
        return app;
    }
}
