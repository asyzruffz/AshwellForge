namespace AshwellForge.Admin.Models;

public record GetStreamsResponse(IList<VideoStream> Streams, int TotalCount);
