using AshwellForge.Core.Utils;
using AshwellForge.Mechanism.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism;

internal class Sender : ISender
{
    readonly IServiceProvider services;

    public Sender(IServiceProvider serviceProvider)
    {
        services = serviceProvider;
    }

    public ValueTask<Result> Send<TOperation>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation
    {
        var handler = services.GetRequiredService<IOperationHandler<TOperation>>();
        return handler.Handle(operation, cancellationToken);
    }

    public ValueTask<Result<TResponse>> Send<TOperation, TResponse>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation<TResponse>
    {
        var handler = services.GetRequiredService<IOperationHandler<TOperation, TResponse>>();
        return handler.Handle(operation, cancellationToken);
    }
}

internal class ApiSender : IApiSender
{
    readonly IServiceProvider services;

    public ApiSender(IServiceProvider serviceProvider)
    {
        services = serviceProvider;
    }

    public ValueTask<ApiResult> Send<TOperation>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation
    {
        var handler = services.GetRequiredService<IApiOperationHandler<TOperation>>();
        return handler.Handle(operation, cancellationToken);
    }

    public ValueTask<ApiResult<TResponse>> Send<TOperation, TResponse>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation<TResponse>
    {
        var handler = services.GetRequiredService<IApiOperationHandler<TOperation, TResponse>>();
        return handler.Handle(operation, cancellationToken);
    }
}
