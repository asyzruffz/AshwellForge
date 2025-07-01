namespace AshwellForge.Mechanism.RtmpServer.Dtos;

public record GetStreamsResponse(IList<Stream> Streams, int TotalCount);
