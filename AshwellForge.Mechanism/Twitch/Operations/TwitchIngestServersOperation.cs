using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;

namespace AshwellForge.Mechanism.Twitch.Operations;

public sealed record GetTwitchIngestServersOperation : IOperation<TwitchIngests>;

internal sealed class GetTwitchIngestServersOperationHandler : IApiOperationHandler<GetTwitchIngestServersOperation, TwitchIngests>
{
    readonly IAshwellForgeStorage storage;
    readonly ITwitchIngestService service;

    public GetTwitchIngestServersOperationHandler(IAshwellForgeStorage storage, ITwitchIngestService ingestService)
    {
        this.storage = storage;
        service = ingestService;
    }

    public async Task<ApiResult<TwitchIngests>> Handle(GetTwitchIngestServersOperation operation, CancellationToken cancellationToken)
    {
        var result = await service.GetIngestServers();
        result.Then(async twitch =>
        {
            foreach (var server in twitch.Ingests)
            {
                await storage.SaveIngestServer(IngestServer.FromTwitch(server));
            }
        });
        return result;
    }
}
