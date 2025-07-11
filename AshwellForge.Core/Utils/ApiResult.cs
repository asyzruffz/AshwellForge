namespace AshwellForge.Core.Utils;

public class ApiResult : ResultE<ApiError>
{
    protected ApiResult(bool isSuccess, ApiError? error)
        : base(isSuccess, error) { }

    public static new ApiResult Ok() => new ApiResult(true, default);
    public static new ApiResult Fail(ApiError error) => new ApiResult(false, error);

    public static implicit operator ApiResult(Result r) =>
        r.Match(Ok, msg => Fail(ApiError.From(msg)));
}

public class ApiResult<T> : ResultE<T, ApiError>
{
    protected ApiResult(bool isSuccess, T? value, ApiError? error)
        : base(isSuccess, value, error) { }

    public static new ApiResult<T> Ok(T value) => new ApiResult<T>(true, value, default);
    public static new ApiResult<T> Fail(ApiError error) => new ApiResult<T>(false, default, error);

    public static implicit operator ApiResult<T>(Result<T> r) =>
        r.Match(Ok, msg => Fail(ApiError.From(msg)));
}
