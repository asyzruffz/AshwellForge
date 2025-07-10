namespace AshwellForge.Core.Utils;

public record ApiError(int StatusCode, string Message)
{
    public static ApiError BadRequest(string Message) => new ApiError(400, Message);
    public static ApiError NotFound(string Message) => new ApiError(404, Message);
    public static ApiError From(string Message)
    {
        var parts = Message.Split(':');
        return new ApiError(int.Parse(parts[0]), parts[1]);
    }

    public override string ToString() => $"{StatusCode}:{Message}";
}
