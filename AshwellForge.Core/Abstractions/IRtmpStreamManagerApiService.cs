using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IRtmpStreamManagerApiService
{
    Task<ApiResult<IEnumerable<VideoStream>>> GetStreamsAsync(GetStreamsRequest request, CancellationToken cancellationToken);
    Task<ApiResult> DeleteStreamAsync(string streamId, CancellationToken cancellationToken);
}
