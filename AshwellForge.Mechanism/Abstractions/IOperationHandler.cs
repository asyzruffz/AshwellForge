using AshwellForge.Core.Utils;

namespace AshwellForge.Mechanism.Abstractions;

public interface IOperationHandler<in TOperation> where TOperation : IOperation
{
    Task<Result> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IOperationHandler<in TOperation, TRespond> where TOperation : IOperation<TRespond>
{
    Task<Result<TRespond>> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation> where TOperation : IOperation
{
    Task<ApiResult> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IApiOperationHandler<in TOperation, TRespond> where TOperation : IOperation<TRespond>
{
    Task<ApiResult<TRespond>> Handle(TOperation operation, CancellationToken cancellationToken);
}
