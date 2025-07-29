using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;

namespace AshwellForge.Mechanism.Twitch.Operations;

public sealed record GetTwitchIngestServersOperation : IOperation<IEnumerable<TwitchIngest>>;

internal sealed class GetTwitchIngestServersOperationHandler : IApiOperationHandler<GetTwitchIngestServersOperation, IEnumerable<TwitchIngest>>
{
    readonly IAshwellForgeStorage storage;
    readonly ITwitchIngestService service;

    public GetTwitchIngestServersOperationHandler(IAshwellForgeStorage storage, ITwitchIngestService ingestService)
    {
        this.storage = storage;
        service = ingestService;
    }

    public async ValueTask<ApiResult<IEnumerable<TwitchIngest>>> Handle(GetTwitchIngestServersOperation operation, CancellationToken cancellationToken)
    {
        var result = await service.GetIngestServers();
        result.Then(async ingests =>
        {
            foreach (var server in ingests)
            {
                await storage.SaveTwitchServer(IngestServer.FromTwitch(server));
            }
        });
        return result;
    }
}
