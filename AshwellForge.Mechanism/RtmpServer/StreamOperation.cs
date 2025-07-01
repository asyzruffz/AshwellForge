using AshwellForge.Mechanism.Core;
using AshwellForge.Mechanism.RtmpServer.Dtos;
using AshwellForge.Mechanism.RtmpServer.Services;
using LiveStreamingServerNet.Networking.Server.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer;

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
        var response = await service.GetStreamsAsync(operation.Parameter);
        return Result<GetStreamsResponse>.Ok(response);
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
        await service.DeleteStreamAsync(operation.StreamId, cancellationToken);
        return Result.Ok();
    }
}
