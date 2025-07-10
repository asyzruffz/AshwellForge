using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet.Networking.Server.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

public sealed record GetStreamsOperation(GetStreamsRequest Parameter) : IOperation<GetStreamsResponse>;

internal sealed class GetStreamsOperationHandler : IOperationHandler<GetStreamsOperation, GetStreamsResponse>
{
    readonly IServer server;

    public GetStreamsOperationHandler(IServer server)
    {
        this.server = server;
    }

    public async Task<Result<GetStreamsResponse>> Handle(GetStreamsOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<RtmpStreamManagerApiService>();
        return await service.GetStreamsAsync(operation.Parameter);
    }
}

public sealed record DeleteStreamOperation(string StreamId) : IOperation;

internal sealed class DeleteStreamOperationHandler : IOperationHandler<DeleteStreamOperation>
{
    readonly IServer server;

    public DeleteStreamOperationHandler(IServer server)
    {
        this.server = server;
    }

    public async Task<Result> Handle(DeleteStreamOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<RtmpStreamManagerApiService>();
        return await service.DeleteStreamAsync(operation.StreamId, cancellationToken);
    }
}
