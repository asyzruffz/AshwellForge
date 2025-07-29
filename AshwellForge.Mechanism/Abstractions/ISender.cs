using AshwellForge.Core.Utils;

namespace AshwellForge.Mechanism.Abstractions;

public interface ISender
{
    ValueTask<Result> Send<TOperation>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation;
    ValueTask<Result<TResponse>> Send<TOperation, TResponse>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation<TResponse>;
}

public interface IApiSender
{
    ValueTask<ApiResult> Send<TOperation>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation;
    ValueTask<ApiResult<TResponse>> Send<TOperation, TResponse>(TOperation operation, CancellationToken cancellationToken = default)
        where TOperation : IOperation<TResponse>;
}
