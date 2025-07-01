namespace AshwellForge.Mechanism.Core;

public class Result
{
    public bool IsSuccess { get; init; }
    public string ErrorMessage { get; init; }

    private Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Ok() => new Result(true, string.Empty);
    public static Result Fail(string errorMessage) => new Result(false, errorMessage);

    public Result OnError(Action<string> action) { if (!IsSuccess) action(ErrorMessage); return this; }

    public Result Then(Func<Result> action) =>
        IsSuccess ? action() : this;
    public Result<TResult> Then<TResult>(Func<Result<TResult>> action) =>
        IsSuccess ? action() : Result<TResult>.Fail(ErrorMessage);
    public void Then(Action action) { if (IsSuccess) action(); }

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<string, TResult> onFailure) =>
        IsSuccess ? onSuccess() : onFailure(ErrorMessage);
}

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public string ErrorMessage { get; init; }

    private T Value;

    private Result(bool isSuccess, T value, string errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T value) => new Result<T>(true, value, string.Empty);
    public static Result<T> Fail(string errorMessage) => new Result<T>(false, default!, errorMessage);

    public T Or(T defaultVal) => IsSuccess ? Value : defaultVal;

    public Result<T> OnError(Action<string> action) { if (!IsSuccess) action(ErrorMessage); return this; }

    public Result Then(Func<T, Result> action) =>
        IsSuccess ? action(Value) : Result.Fail(ErrorMessage);
    public Result<TResult> Then<TResult>(Func<T, Result<TResult>> action) =>
        IsSuccess ? action(Value) : Result<TResult>.Fail(ErrorMessage);
    public void Then(Action<T> action) { if (IsSuccess) action(Value); }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure) =>
        IsSuccess ? onSuccess(Value) : onFailure(ErrorMessage);
}
