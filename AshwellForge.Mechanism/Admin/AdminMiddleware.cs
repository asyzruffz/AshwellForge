using Microsoft.AspNetCore.Http;

namespace AshwellForge.Mechanism.Admin;

public class AdminMiddleware
{
    private readonly RequestDelegate next;
    private readonly AdminOptions options;

    public AdminMiddleware(RequestDelegate next, AdminOptions? options)
    {
        this.next = next;
        this.options = options ?? new AdminOptions();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (Validate(context) && await TryServeAdminPanelUI(context))
            return;

        await next.Invoke(context);
    }

    private async Task<bool> TryServeAdminPanelUI(HttpContext context)
    {
        var fileContext = new FileContext(options);
        return await fileContext.ServeAdminPanelUI(context);
    }

    private bool Validate(HttpContext context)
    {
        if (context.GetEndpoint() != null)
            return false;

        if (context.Response.ContentLength != null)
            return false;

        return true;
    }
}
