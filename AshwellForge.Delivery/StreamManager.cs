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
        [AsParameters] GetStreamsRequest request,
        [FromServices] IApiOperationHandler<GetStreamsOperation, IEnumerable<VideoStream>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetStreamsOperation(request), cancellationToken);
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
        [AsParameters] GetIngestsRequest request,
        [FromServices] IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetIngestServersOperation(request.ToParam()), cancellationToken);
        return result.ToHttpResult();
    }

    public static async Task<IResult> SaveIngestServer(
        [FromBody] IngestServer server,
        [FromServices] IApiOperationHandler<SaveIngestServerOperation> handler,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(server.ToString());
        var result = await handler.Handle(new SaveIngestServerOperation(server), cancellationToken);
        return result.ToHttpResult();
    }
}
