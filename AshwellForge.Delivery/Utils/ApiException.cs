namespace AshwellForge.Delivery.Utils;

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ErrorResponse ErrorResponse { get; }

    public ApiException(int statusCode, string message)
        : this(statusCode, new ErrorResponse(message))
    {
    }

    public ApiException(int statusCode, string message, IReadOnlyDictionary<string, string> errors)
        : this(statusCode, new ErrorResponse(message, errors))
    {
    }

    public ApiException(int statusCode, ErrorResponse errorResponse)
        : base(errorResponse.Message)
    {
        StatusCode = statusCode;
        ErrorResponse = errorResponse;
    }

    public ApiException()
        : this(500, "Unknown error.")
    {
    }

    public ApiException(string? message)
        : this(500, message ?? "Unknown error.")
    {
    }

    public ApiException(string? message, Exception? innerException)
        : this(500, message ?? innerException?.Message ?? "Unknown error.")
    {
    }
}
