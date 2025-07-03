using Microsoft.AspNetCore.Http;
using System.Text;

namespace AshwellForge.Mechanism.Admin;

internal struct FileContext
{
    private readonly AdminOptions options;

    public FileContext(AdminOptions options)
    {
        ArgumentNullException.ThrowIfNull(options.FileProvider);
        ArgumentNullException.ThrowIfNull(options.ContentTypeProvider);

        this.options = options;
    }

    public async Task<bool> ServeAdminPanelUI(HttpContext context)
    {
        if (await TryServeEnvConfig(context))
            return true;

        if (await TryServeFile(context))
            return true;

        if (await TryServeIndex(context))
            return true;

        return false;
    }

    private async Task<bool> TryServeEnvConfig(HttpContext context)
    {
        var subPath = context.Request.Path.ToString();

        if (subPath != $"{AdminConstants.FileBasePath}/env-config.js")
            return false;

        var envConfig = CreateEnvConfig();
        context.Response.StatusCode = 200;
        context.Response.ContentType = "text/javascript";
        await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(envConfig));

        return true;
    }

    private async Task<bool> TryServeFile(HttpContext context)
    {
        var subPath = context.Request.Path;

        if (!context.Request.Path.StartsWithSegments(AdminConstants.FileBasePath))
            return false;

        return await TryServeFile(context, subPath.ToString().Substring(AdminConstants.FileBasePath.Length));
    }

    private async Task<bool> TryServeIndex(HttpContext context)
    {
        if (options.BasePath != "/" && !context.Request.Path.StartsWithSegments(options.BasePath))
            return false;

        return await TryServeFile(context, "/index.html");
    }

    private async Task<bool> TryServeFile(HttpContext context, PathString subPath)
    {
        if (!options.ContentTypeProvider!.TryGetContentType(subPath, out var contentType))
            return false;

        var fileInfo = options.FileProvider!.GetFileInfo(subPath);

        if (!fileInfo.Exists)
            return false;

        context.Response.StatusCode = 200;
        context.Response.ContentType = contentType;
        await context.Response.SendFileAsync(fileInfo);

        return true;
    }

    private string CreateEnvConfig()
    {
        var builder = new StringBuilder();

        builder.Append("window._env = {");

        foreach (var (key, value) in options.GetParameters())
        {
            switch (value)
            {
                case bool boolValue:
                    builder.Append($"{key}:{boolValue.ToString().ToLower()},");
                    break;
                case string stringValue:
                    builder.Append($"{key}:\"{stringValue}\",");
                    break;
                default:
                    builder.Append($"{key}:{value},");
                    break;
            }
        }

        builder.Append("}");

        return builder.ToString();
    }
}
