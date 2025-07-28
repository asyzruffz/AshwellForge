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
        [FromServices] IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetIngestServersOperation(request.ToParam()), cancellationToken);
        return result.ToHttpResult();
    }
}
