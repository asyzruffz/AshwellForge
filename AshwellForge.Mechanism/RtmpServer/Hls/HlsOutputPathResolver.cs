using LiveStreamingServerNet.StreamProcessor.Hls.Contracts;

namespace AshwellForge.Mechanism.RtmpServer.Hls;

internal class HlsOutputPathResolver : IHlsOutputPathResolver
{
    private readonly string outputDir;

    public HlsOutputPathResolver(string outputDir)
    {
        this.outputDir = outputDir;
    }

    public ValueTask<string> ResolveOutputPath(IServiceProvider services, Guid contextIdentifier, string streamPath, IReadOnlyDictionary<string, string> streamArguments)
    {
        return ValueTask.FromResult(Path.Combine(outputDir, contextIdentifier.ToString(), "output.m3u8"));
    }
}
