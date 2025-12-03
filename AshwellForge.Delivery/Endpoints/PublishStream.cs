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

internal class PublishStream : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/streams", Execute)
            .WithTags(Tags.Admin);
    }

    async Task<IResult> Execute(
        [FromBody] PublishStreamRequest request,
        [FromServices] IApiSender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new PublishStreamOperation(request.Endpoint, request.StreamKey), cancellationToken);
        return result.ToHttpResult();
    }
}
