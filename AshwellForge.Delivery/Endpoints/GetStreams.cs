using AshwellForge.Core.Data;
using AshwellForge.Delivery.Abstractions;
using AshwellForge.Delivery.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery.Endpoints;

internal class GetStreams : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/streams", Execute)
            .WithTags(Tags.Admin);
    }

    async Task<IResult> Execute(
        [AsParameters] GetStreamsRequest request,
        [FromServices] IApiOperationHandler<GetStreamsOperation, IEnumerable<VideoStream>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetStreamsOperation(request), cancellationToken);
        return result.ToHttpResult();
    }
}
