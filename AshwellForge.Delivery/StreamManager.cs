using AshwellForge.Core.Data;
using AshwellForge.Delivery.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AshwellForge.Delivery;

internal static class StreamManager
{
    public static async Task<IResult> GetStreams(
        [AsParameters] GetStreamsRequest parameter,
        [FromServices] IApiOperationHandler<GetStreamsOperation, IEnumerable<VideoStream>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetStreamsOperation(parameter), cancellationToken);
        return result.ToHttpResult();
    }

    public static async Task<IResult> DeleteStream(
        [FromQuery] string streamId,
        [FromServices] IApiOperationHandler<DeleteStreamOperation> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new DeleteStreamOperation(streamId), cancellationToken);
        return result.ToHttpResult();
    }

    public static async Task<IResult> GetIngestServers(
        [AsParameters] bool refresh,
        [FromServices] IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetIngestServersOperation(refresh), cancellationToken);
        return result.ToHttpResult();
    }
}
