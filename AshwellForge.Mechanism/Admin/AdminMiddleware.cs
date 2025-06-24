namespace AshwellForge.Mechanism.Admin;

public class AdminMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AdminOptions _options;

    public AdminMiddleware(RequestDelegate next, AdminOptions? options)
    {
        _next = next;
        _options = options ?? new AdminOptions();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (Validate(context) && await TryServeAdminPanelUI(context))
            return;

        await _next.Invoke(context);
    }

    private async Task<bool> TryServeAdminPanelUI(HttpContext context)
    {
        var fileContext = new FileContext(_options);
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
