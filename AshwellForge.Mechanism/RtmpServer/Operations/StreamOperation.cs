using AshwellForge.Core.Abstractions;
using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using LiveStreamingServerNet.Networking.Server.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

public sealed record GetStreamsOperation(GetStreamsRequest Parameter) : IOperation<GetStreamsResponse>;

internal sealed class GetStreamsOperationHandler : IApiOperationHandler<GetStreamsOperation, GetStreamsResponse>
{
    readonly IServer server;

    public GetStreamsOperationHandler(IServer server)
    {
        this.server = server;
    }

    public async Task<ApiResult<GetStreamsResponse>> Handle(GetStreamsOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<IRtmpStreamManagerApiService>();
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

    public async Task<ApiResult> Handle(DeleteStreamOperation operation, CancellationToken cancellationToken)
    {
        var service = server.Services.GetRequiredService<IRtmpStreamManagerApiService>();
        return await service.DeleteStreamAsync(operation.StreamId, cancellationToken);
    }
}
