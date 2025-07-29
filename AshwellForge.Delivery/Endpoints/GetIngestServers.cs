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

internal class GetIngestServers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/ingests", Execute)
            .WithTags(Tags.Admin);
    }

    async Task<IResult> Execute(
        [AsParameters] GetIngestsRequest request,
        [FromServices] IApiSender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send<GetIngestServersOperation, IEnumerable<IngestServer>>(new GetIngestServersOperation(request.ToParam()), cancellationToken);
        return result.ToHttpResult();
    }
}
