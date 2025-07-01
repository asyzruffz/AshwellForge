using AshwellForge.Mechanism.Core;
using AshwellForge.Mechanism.RtmpServer.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Mechanism.RtmpServer;

internal static class StreamManagerApiEndpoints
{
    public static IEndpointRouteBuilder MapStreamManagerApiEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder endpoints = builder.MapGroup("api/v1/streams").AddEndpointFilter<ApiExceptionEndpointFilter>();
        endpoints.MapGet("/", GetStreams);
        endpoints.MapDelete("/", DeleteStream);
        return builder;
    }

    public static async Task<IResult> GetStreams(
        [AsParameters] GetStreamsRequest parameter,
        IOperationHandler<GetStreamsOperation, GetStreamsResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetStreamsOperation(parameter), cancellationToken);

        return result.Match<IResult>(
            onSuccess: response => TypedResults.Ok(response),
            onFailure: msg => TypedResults.InternalServerError(msg));
    }

    public static async Task<IResult> DeleteStream(
        [FromQuery] string streamId,
        IOperationHandler<DeleteStreamOperation> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new DeleteStreamOperation(streamId), cancellationToken);

        return result.Match<IResult>(
            onSuccess: TypedResults.Ok,
            onFailure: msg => TypedResults.InternalServerError(msg));
    }
}
