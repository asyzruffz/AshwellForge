using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IRtmpStreamManagerApiService
{
    Task<Result<GetStreamsResponse>> GetStreamsAsync(GetStreamsRequest request, CancellationToken cancellationToken);
    Task<CustomResult<ApiError>> DeleteStreamAsync(string streamId, CancellationToken cancellationToken);
}
