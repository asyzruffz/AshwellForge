namespace AshwellForge.Core.Data;

public record GetStreamsResponse(IList<Stream> Streams, int TotalCount);
