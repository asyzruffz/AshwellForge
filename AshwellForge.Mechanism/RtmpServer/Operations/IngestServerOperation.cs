using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

public sealed record GetIngestServersOperation(GetIngestsParam Parameter) : IOperation<IEnumerable<IngestServer>>;

internal sealed class GetIngestServersOperationHandler : IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>>
{
    readonly IIngestServerService service;

    public GetIngestServersOperationHandler(IIngestServerService service)
    {
        this.service = service;
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> Handle(GetIngestServersOperation operation, CancellationToken cancellationToken)
    {
        switch (operation.Parameter.Kind)
        {
            case IngestServerKind.Saved:
                return await service.GetSavedIngestServers(cancellationToken);
            case IngestServerKind.Twitch:
                return await service.GetTwitchIngestServers(operation.Parameter.ForceRefresh, cancellationToken);
            default:
                return ApiResult<IEnumerable<IngestServer>>.Fail(ApiError.BadRequest("Invalid ingest server kind"));
        }
    }
}
