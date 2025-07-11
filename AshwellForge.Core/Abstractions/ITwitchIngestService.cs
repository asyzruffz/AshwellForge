using AshwellForge.Core.Data;
using AshwellForge.Core.Utils;

namespace AshwellForge.Core.Abstractions;

public interface ITwitchIngestService
{
    Task<ApiResult<TwitchIngests>> GetIngestServers();
}
