using AshwellForge.Core.Utils;
using AshwellForge.Delivery.Utils;
using Microsoft.AspNetCore.Http;

namespace AshwellForge.Delivery;

public class ApiExceptionEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (Exception ex)
        {
#if DEBUG
            var result = ApiResult.Fail(ApiError.Internal(ex.Message));
#else
            var result = ApiResult.Fail(ApiError.Internal("Unknown error"));
#endif
            return result.ToHttpResult();
        }
    }
}
