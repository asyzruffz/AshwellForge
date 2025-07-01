using AshwellForge.Mechanism.Admin;
using Microsoft.AspNetCore.Builder;

namespace AshwellForge.Mechanism;

public static class Extensions
{
    /// <summary>
    /// Adds the Admin Panel UI middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <returns>The WebApplication instance for method chaining.</returns>
    public static WebApplication UseAdminPanelUI(this WebApplication app)
        => UseAdminPanelUI(app, new AdminOptions());

    /// <summary>
    /// Adds the Admin Panel UI middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <param name="options">Optional configuration options for the Admin Panel UI.</param>
    /// <returns>The WebApplication instance for method chaining.</returns>
    public static WebApplication UseAdminPanelUI(this WebApplication app, AdminOptions options)
    {
        app.UseMiddleware<AdminMiddleware>(options);
        return app;
    }
}
