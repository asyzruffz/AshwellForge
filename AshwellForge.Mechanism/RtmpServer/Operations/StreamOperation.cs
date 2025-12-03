using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using LiveStreamingServerNet.Networking.Server.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

public sealed record GetStreamsOperation(GetStreamsRequest Parameter) : IOperation<IEnumerable<VideoStream>>;

internal sealed class GetStreamsOperationHandler : IApiOperationHandler<GetStreamsOperation, IEnumerable<VideoStream>>
{
    readonly IServer server;

    public GetStreamsOperationHandler(IServer server)
    {
        this.server = server;
    }

    public async ValueTask<ApiResult<IEnumerable<VideoStream>>> Handle(GetStreamsOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<IStreamManagerApiService>();
        return await service.GetStreamsAsync(operation.Parameter, cancellationToken);
    }
}

public sealed record DeleteStreamOperation(string StreamId) : IOperation;

internal sealed class DeleteStreamOperationHandler : IApiOperationHandler<DeleteStreamOperation>
{
    readonly IServer server;

    public DeleteStreamOperationHandler(IServer server)
    {
        this.server = server;
    }

    public async ValueTask<ApiResult> Handle(DeleteStreamOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<IStreamManagerApiService>();
        return await service.DeleteStreamAsync(operation.StreamId, cancellationToken);
    }
}

public sealed record PublishStreamOperation(string Endpoint, string StreamKey) : IOperation;

public class PublishStreamOperationHandler : IApiOperationHandler<PublishStreamOperation>
{
    readonly IStreamManagerApiService service;

    public PublishStreamOperationHandler(IStreamManagerApiService streamManagerApiService)
    {
        service = streamManagerApiService;
    }

    public async ValueTask<ApiResult> Handle(PublishStreamOperation operation, CancellationToken cancellationToken)
    {
        return await service.PublishStreamAsync(operation.Endpoint, cancellationToken);
    }
}
