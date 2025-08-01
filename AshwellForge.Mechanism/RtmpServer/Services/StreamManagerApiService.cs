﻿using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.RtmpServer.Utils;
using LiveStreamingServerNet.Rtmp.Server.Contracts;

namespace AshwellForge.Mechanism.RtmpServer.Services;

internal class StreamManagerApiService : IStreamManagerApiService
{
    private readonly IRtmpStreamInfoManager streamInfoManager;

    public StreamManagerApiService(IRtmpStreamInfoManager streamInfoManager)
    {
        this.streamInfoManager = streamInfoManager;
    }

    public Task<ApiResult<IEnumerable<VideoStream>>> GetStreamsAsync(GetStreamsRequest parameter, CancellationToken cancellationToken)
    {
        var (page, pageSize, filter) = parameter;

        var streams = streamInfoManager.GetStreamInfos();

        if (!string.IsNullOrWhiteSpace(filter))
            streams = streams.Where(x => x.StreamPath.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

        var result = streams
            .OrderByDescending(x => x.StartTime)
            .Skip(Math.Max(0, page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => s.ToDto())
            .ToList();

        return Task.FromResult(ApiResult<IEnumerable<VideoStream>>.Ok(result));
    }

    public async Task<ApiResult> DeleteStreamAsync(string streamId, CancellationToken cancellationToken)
    {
        var splitIndex = streamId.IndexOf('@');

        if (splitIndex == -1 || !uint.TryParse(streamId.Substring(0, splitIndex), out var clientId))
            return ApiResult.Fail(ApiError.BadRequest("Invalid stream id format."));

        var streamPath = streamId.Substring(splitIndex + 1);
        var stream = streamInfoManager.GetStreamInfo(streamPath);

        if (stream == null || stream.Publisher.Id != clientId)
            return ApiResult.Fail(ApiError.NotFound($"Stream ({streamId}) not found."));

        await stream.Publisher.DisconnectAsync(cancellationToken);

        return ApiResult.Ok();
    }
}
