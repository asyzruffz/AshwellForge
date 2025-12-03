using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IStreamManagerApiService
{
    Task<ApiResult<IEnumerable<VideoStream>>> GetStreamsAsync(GetStreamsRequest parameter, CancellationToken cancellationToken);
    Task<ApiResult> DeleteStreamAsync(string streamId, CancellationToken cancellationToken);
    Task<ApiResult> PublishStreamAsync(string url, CancellationToken cancellationToken);
}
