namespace AshwellForge.Core.Data;

public record GetStreamsRequest(int page, int pageSize, string? filter);
