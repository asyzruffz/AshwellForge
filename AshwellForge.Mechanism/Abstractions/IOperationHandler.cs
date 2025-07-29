using AshwellForge.Core.Utils;

namespace AshwellForge.Mechanism.Abstractions;

public interface IOperationHandler<in TOperation> where TOperation : IOperation
{
    ValueTask<Result> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IOperationHandler<in TOperation, TRespond> where TOperation : IOperation<TRespond>
{
    ValueTask<Result<TRespond>> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation> where TOperation : IOperation
{
    ValueTask<ApiResult> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation, TRespond> where TOperation : IOperation<TRespond>
{
    ValueTask<ApiResult<TRespond>> Handle(TOperation operation, CancellationToken cancellationToken);
}
