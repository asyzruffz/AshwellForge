namespace AshwellForge.Mechanism.Core;

public interface IOperationHandler<in TOperation> where TOperation : IOperation
{
    Task<Result> Handle(TOperation operation, CancellationToken cancellationToken);
}

public interface IOperationHandler<in TOperation, TRespond> where TOperation : IOperation<TRespond>
{
    Task<Result<TRespond>> Handle(TOperation operation, CancellationToken cancellationToken);
}
