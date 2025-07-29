using AshwellForge.Core.Utils;

namespace AshwellForge.Mechanism.Abstractions;

public interface IOperationHandler<in TOperation> where TOperation : IOperation
{
    ValueTask<Result> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IOperationHandler<in TOperation, TResponse> where TOperation : IOperation<TResponse>
{
    ValueTask<Result<TResponse>> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation> where TOperation : IOperation
{
    ValueTask<ApiResult> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation, TResponse> where TOperation : IOperation<TResponse>
{
    ValueTask<ApiResult<TResponse>> Handle(TOperation operation, CancellationToken cancellationToken);
}
