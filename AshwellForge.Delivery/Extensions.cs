using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery;

public static class Extensions
{
    public static IEndpointRouteBuilder MapAdminApiEndpoints(this IEndpointRouteBuilder builder, string apiBaseUri)
    {
        RouteGroupBuilder endpoints = builder.MapGroup(apiBaseUri)
            .AddEndpointFilter<ApiExceptionEndpointFilter>();

        endpoints.MapGet("/streams", StreamManager.GetStreams);
        endpoints.MapDelete("/streams", StreamManager.DeleteStream);

        endpoints.MapGet("/ingests", StreamManager.GetIngestServers);

        return builder;
    }
}
