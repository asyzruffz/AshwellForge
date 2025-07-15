using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IIngestServerService
{
    Task<ApiResult<IEnumerable<IngestServer>>> GetSavedIngestServers(CancellationToken cancellationToken);
    Task<ApiResult<IEnumerable<IngestServer>>> GetTwitchIngestServers(bool forceRefresh, CancellationToken cancellationToken);
}
