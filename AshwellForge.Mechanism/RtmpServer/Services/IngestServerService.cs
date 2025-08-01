﻿using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.Twitch.Operations;

namespace AshwellForge.Mechanism.RtmpServer.Services;

internal class IngestServerService : IIngestServerService
{
    readonly IAshwellForgeStorage storage;
    readonly IApiOperationHandler<GetTwitchIngestServersOperation, IEnumerable<TwitchIngest>> twitchOperation;

    public IngestServerService(
        IAshwellForgeStorage storage,
        IApiOperationHandler<GetTwitchIngestServersOperation, IEnumerable<TwitchIngest>> twitchOperation)
    {
        this.storage = storage;
        this.twitchOperation = twitchOperation;
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> GetSavedIngestServers(CancellationToken cancellationToken)
    {
        var servers = await storage.GetSavedIngestServers();
        return ApiResult<IEnumerable<IngestServer>>.Ok(servers);
    }

    public async Task<ApiResult<IEnumerable<IngestServer>>> GetTwitchIngestServers(bool forceRefresh, CancellationToken cancellationToken)
    {
        if (forceRefresh || !await storage.HasTwitchServers())
        {
            var result = await twitchOperation.Handle(new GetTwitchIngestServersOperation(), cancellationToken);
            if (!result.IsSuccess)
            {
                return ApiResult<IEnumerable<IngestServer>>.Fail(result.Error!);
            }
        }

        var servers = await storage.GetTwitchIngestServers();
        return ApiResult<IEnumerable<IngestServer>>.Ok(servers);
    }

    public async Task<ApiResult> SaveIngestServer(IngestServer server, CancellationToken cancellationToken)
    {
        await storage.SaveIngestServer(server);
        return ApiResult.Ok();
    }
}
