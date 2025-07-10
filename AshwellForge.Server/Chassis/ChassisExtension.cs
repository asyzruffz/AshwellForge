using AshwellForge.Delivery;
using LiveStreamingServerNet.Flv.Installer;

namespace AshwellForge.Server.Chassis;

internal static class ChassisExtension
{
    public static IServiceCollection AddChassis(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();
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

        app.MapStreamManagerEndpoints(options.StreamsBaseUri);
        app.MapAdminComponents();
        return app;
    }

    static IEndpointRouteBuilder MapAdminComponents(this IEndpointRouteBuilder app)
    {
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(Admin.AssemblyReference.Assembly);
        return app;
    }
}
