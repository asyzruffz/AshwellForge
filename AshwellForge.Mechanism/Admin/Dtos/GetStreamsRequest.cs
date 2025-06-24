namespace AshwellForge.Mechanism.Admin.Dtos;

public record GetStreamsRequest(int page, int pageSize, string? filter);
