using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery;

public static class Extensions
{
    public static IEndpointRouteBuilder MapStreamManagerEndpoints(this IEndpointRouteBuilder builder, string streamsBaseUri) =>
        builder.MapStreamManagerApiEndpoints(streamsBaseUri);
}
