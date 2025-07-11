namespace AshwellForge.Core.Utils;

public record ApiError(int StatusCode, string Message)
{
    public static ApiError BadRequest(string Message) => new ApiError(400, Message);
    public static ApiError NotFound(string Message) => new ApiError(404, Message);
    public static ApiError Internal(string Message) => new ApiError(500, Message);
    public static ApiError From(string Message)
    {
        int code = 500;

        var parts = Message.Split(':');
        if (parts.Length > 1)
        {
            int.TryParse(parts[0], out code);
            return new ApiError(code, string.Join(':', parts.Skip(1)));
        }

        return new ApiError(code, Message);
    }

    public override string ToString() => $"{StatusCode}:{Message}";
}
