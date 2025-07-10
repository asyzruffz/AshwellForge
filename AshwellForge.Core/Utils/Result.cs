namespace AshwellForge.Core.Utils;

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

    public static implicit operator CustomResult<string>(Result r) =>
        r.Match(CustomResult<string>.Ok, CustomResult<string>.Fail);

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

    private T? Value;

    private Result(bool isSuccess, T? value, string errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T value) => new Result<T>(true, value, string.Empty);
    public static Result<T> Fail(string errorMessage) => new Result<T>(false, default, errorMessage);

    public static implicit operator CustomResult<T, string>(Result<T> r) =>
        r.Match(CustomResult<T, string>.Ok, CustomResult<T, string>.Fail);

    public T Or(T defaultVal) => IsSuccess ? Value! : defaultVal;

    public Result<T> OnError(Action<string> action) { if (!IsSuccess) action(ErrorMessage); return this; }

    public Result Then(Func<T, Result> action) =>
        IsSuccess ? action(Value!) : Result.Fail(ErrorMessage);
    public Result<TResult> Then<TResult>(Func<T, Result<TResult>> action) =>
        IsSuccess ? action(Value!) : Result<TResult>.Fail(ErrorMessage);
    public void Then(Action<T> action) { if (IsSuccess) action(Value!); }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure) =>
        IsSuccess ? onSuccess(Value!) : onFailure(ErrorMessage);
}

public class CustomResult<E>
{
    public bool IsSuccess { get; init; }
    public E? Error { get; init; }

    private CustomResult(bool isSuccess, E? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static CustomResult<E> Ok() => new CustomResult<E>(true, default);
    public static CustomResult<E> Fail(E error) => new CustomResult<E>(false, error);

    public static implicit operator Result(CustomResult<E> cr) =>
        cr.Match(Result.Ok, e => Result.Fail(e!.ToString() ?? string.Empty));

    public CustomResult<E> OnError(Action<E> action) { if (!IsSuccess) action(Error!); return this; }

    public CustomResult<E> Then(Func<CustomResult<E>> action) =>
        IsSuccess ? action() : this;
    public CustomResult<TResult, E> Then<TResult>(Func<CustomResult<TResult, E>> action) =>
        IsSuccess ? action() : CustomResult<TResult, E>.Fail(Error!);
    public void Then(Action action) { if (IsSuccess) action(); }

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<E, TResult> onFailure) =>
        IsSuccess ? onSuccess() : onFailure(Error!);
}

public class CustomResult<T, E>
{
    public bool IsSuccess { get; init; }
    public E? Error { get; init; }

    private T? Value;

    private CustomResult(bool isSuccess, T? value, E? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static CustomResult<T, E> Ok(T value) => new CustomResult<T, E>(true, value, default);
    public static CustomResult<T, E> Fail(E error) => new CustomResult<T, E>(false, default, error);

    public static implicit operator Result<T>(CustomResult<T, E> cr) =>
        cr.Match(Result<T>.Ok, e => Result<T>.Fail(e!.ToString() ?? string.Empty));

    public T Or(T defaultVal) => IsSuccess ? Value! : defaultVal;

    public CustomResult<T, E> OnError(Action<E> action) { if (!IsSuccess) action(Error!); return this; }

    public CustomResult<E> Then(Func<T, CustomResult<E>> action) =>
        IsSuccess ? action(Value!) : CustomResult<E>.Fail(Error!);
    public CustomResult<TResult, E> Then<TResult>(Func<T, CustomResult<TResult, E>> action) =>
        IsSuccess ? action(Value!) : CustomResult<TResult, E>.Fail(Error!);
    public void Then(Action<T> action) { if (IsSuccess) action(Value!); }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<E, TResult> onFailure) =>
        IsSuccess ? onSuccess(Value!) : onFailure(Error!);
}
