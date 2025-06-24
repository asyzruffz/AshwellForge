namespace AshwellForge.Mechanism.Admin.Dtos;

public record GetStreamsResponse(IList<Stream> Streams, int TotalCount);
