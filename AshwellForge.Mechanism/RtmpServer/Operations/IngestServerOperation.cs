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

    public async ValueTask<ApiResult<IEnumerable<IngestServer>>> Handle(GetIngestServersOperation operation, CancellationToken cancellationToken)
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

public sealed record SaveIngestServerOperation(IngestServer Server) : IOperation;

internal sealed class SaveIngestServerOperationHandler : IApiOperationHandler<SaveIngestServerOperation>
{
    readonly IIngestServerService service;

    public SaveIngestServerOperationHandler(IIngestServerService service)
    {
        this.service = service;
    }

    public async ValueTask<ApiResult> Handle(SaveIngestServerOperation operation, CancellationToken cancellationToken)
    {
        return await service.SaveIngestServer(operation.Server, cancellationToken);
    }
}
