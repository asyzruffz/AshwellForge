namespace AshwellForge.Core.RtmpServer;

public interface IStreamReader
{
    ValueTask ReadExactlyAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default(CancellationToken));
}
