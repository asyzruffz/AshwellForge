using AshwellForge.Mechanism.Core;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AshwellForge.Mechanism.RtmpServer;

public class ApiExceptionEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (ApiException ex)
        {
            return Results.Json(ex.ErrorResponse, (JsonSerializerOptions?)null, null, ex.StatusCode);
        }
    }
}
