using AshwellForge.Core.Data;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery;

internal static class StreamManagerApiEndpoints
{
    public static IEndpointRouteBuilder MapStreamManagerApiEndpoints(this IEndpointRouteBuilder builder, string streamsBaseUri)
    {
        RouteGroupBuilder endpoints = builder.MapGroup(streamsBaseUri).AddEndpointFilter<ApiExceptionEndpointFilter>();
        endpoints.MapGet("/", GetStreams);
        endpoints.MapDelete("/", DeleteStream);
        return builder;
    }

    public static async Task<IResult> GetStreams(
        [AsParameters] GetStreamsRequest parameter,
        IApiOperationHandler<GetStreamsOperation, GetStreamsResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetStreamsOperation(parameter), cancellationToken);

        return result.Match<IResult>(
            onSuccess: response => TypedResults.Ok(response),
            onFailure: err => TypedResults.InternalServerError(err.Message));
    }

    public static async Task<IResult> DeleteStream(
        [FromQuery] string streamId,
        IApiOperationHandler<DeleteStreamOperation> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new DeleteStreamOperation(streamId), cancellationToken);

        return result.Match(
            onSuccess: TypedResults.Ok,
            onFailure: err => Results.StatusCode(err.StatusCode));
    }
}
