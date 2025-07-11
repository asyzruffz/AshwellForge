using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

public sealed record GetIngestServersOperation(bool ForceRefresh = false) : IOperation<IEnumerable<IngestServer>>;

internal sealed class GetIngestServersOperationHandler : IApiOperationHandler<GetIngestServersOperation, IEnumerable<IngestServer>>
{
    readonly IIngestServerService service;

    public GetIngestServersOperationHandler(IIngestServerService service)
    {
        this.service = service;
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> Handle(GetIngestServersOperation operation, CancellationToken cancellationToken)
    {
        return await service.GetIngestServers(operation.ForceRefresh, cancellationToken);
    }
}
