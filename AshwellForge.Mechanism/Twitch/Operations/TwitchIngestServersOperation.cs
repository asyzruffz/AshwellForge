using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;

namespace AshwellForge.Mechanism.Twitch.Operations;

public sealed record GetTwitchIngestServersOperation : IOperation<TwitchIngests>;

internal sealed class GetTwitchIngestServersOperationHandler : IOperationHandler<GetTwitchIngestServersOperation, TwitchIngests>
{
    readonly ITwitchIngestService service;

    public GetTwitchIngestServersOperationHandler(ITwitchIngestService ingestService)
    {
        service = ingestService;
    }

    public async Task<Result<TwitchIngests>> Handle(GetTwitchIngestServersOperation operation, CancellationToken cancellationToken)
    {
        return await service.GetIngestServers();
    }
}
