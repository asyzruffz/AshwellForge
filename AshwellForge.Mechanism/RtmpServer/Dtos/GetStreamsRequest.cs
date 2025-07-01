namespace AshwellForge.Mechanism.RtmpServer.Dtos;

public record GetStreamsRequest(int page, int pageSize, string? filter);
