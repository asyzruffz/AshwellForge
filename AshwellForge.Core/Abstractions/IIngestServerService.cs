using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface IIngestServerService
{
    Task<ApiResult<IEnumerable<IngestServer>>> GetIngestServers(bool forceRefresh);
}
