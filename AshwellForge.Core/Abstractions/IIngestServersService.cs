using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IIngestServersService
{
    Task<ApiResult<IEnumerable<IngestServer>>> GetIngestServers(bool forceRefresh);
}
