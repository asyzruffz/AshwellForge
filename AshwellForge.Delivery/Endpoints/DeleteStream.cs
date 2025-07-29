using AshwellForge.Delivery.Abstractions;
using AshwellForge.Delivery.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Operations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery.Endpoints;

internal class DeleteStream : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/streams", Execute)
            .WithTags(Tags.Admin);
    }

    async Task<IResult> Execute(
        [FromQuery] string streamId,
        [FromServices] IApiSender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteStreamOperation(streamId), cancellationToken);
        return result.ToHttpResult();
    }
}
