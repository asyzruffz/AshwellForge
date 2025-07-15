using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.Twitch.Operations;

namespace AshwellForge.Mechanism.RtmpServer.Services;

internal class IngestServerService : IIngestServerService
{
    readonly IAshwellForgeStorage storage;
    readonly IApiOperationHandler<GetTwitchIngestServersOperation, TwitchIngests> twitchOperation;

    public IngestServerService(
        IAshwellForgeStorage storage,
        IApiOperationHandler<GetTwitchIngestServersOperation, TwitchIngests> twitchOperation)
    {
        this.storage = storage;
        this.twitchOperation = twitchOperation;
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> GetSavedIngestServers(CancellationToken cancellationToken)
    {
        var servers = await storage.GetIngestServers();
        return ApiResult<IEnumerable<IngestServer>>.Ok(servers);
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> GetTwitchIngestServers(bool forceRefresh, CancellationToken cancellationToken)
    {
        if (forceRefresh || !await storage.HasIngestServers())
        {
            var result = await twitchOperation.Handle(new GetTwitchIngestServersOperation(), cancellationToken);
            if (!result.IsSuccess)
            {
                return ApiResult<IEnumerable<IngestServer>>.Fail(result.Error!);
            }
        }

        var servers = await storage.GetIngestServers();
        return ApiResult<IEnumerable<IngestServer>>.Ok(servers);
    }
}
