using AshwellForge.Core.Utils;
using Microsoft.AspNetCore.Http;

namespace AshwellForge.Delivery.Utils;

internal static class ApiUtils
{
    public static IResult ToHttpResult(this ApiResult result) =>
        result.Match(TypedResults.Ok, ToHttpResult);
    public static IResult ToHttpResult<T>(this ApiResult<T> result) =>
        result.Match(TypedResults.Ok, ToHttpResult);

    static IResult ToHttpResult(this ApiError err) =>
        Results.Problem(err.Message, statusCode: err.StatusCode);
}
