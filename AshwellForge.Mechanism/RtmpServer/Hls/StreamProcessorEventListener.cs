using LiveStreamingServerNet.StreamProcessor.Contracts;
using LiveStreamingServerNet.Utilities.Contracts;
using Microsoft.Extensions.Logging;

namespace AshwellForge.Mechanism.RtmpServer.Hls;

internal class StreamProcessorEventListener : IStreamProcessorEventHandler
{
    private readonly string outputDir;
    private readonly ILogger logger;

    public StreamProcessorEventListener(string outputDir, ILogger<StreamProcessorEventListener> logger)
    {
        this.outputDir = outputDir;
        this.logger = logger;
    }

    public Task OnStreamProcessorStartedAsync(IEventContext context, string processor, Guid identifier, uint clientId, string inputPath, string outputPath, string streamPath, IReadOnlyDictionary<string, string> streamArguments)
    {
        outputPath = Path.GetRelativePath(outputDir, outputPath);
        logger.LogInformation($"[{identifier}] Streaming processor {processor} started: {inputPath} -> {outputPath}");
        return Task.CompletedTask;
    }

    public Task OnStreamProcessorStoppedAsync(IEventContext context, string processor, Guid identifier, uint clientId, string inputPath, string outputPath, string streamPath, IReadOnlyDictionary<string, string> streamArguments)
    {
        outputPath = Path.GetRelativePath(outputDir, outputPath);
        logger.LogInformation($"[{identifier}] Streaming processor {processor} stopped: {inputPath} -> {outputPath}");
        return Task.CompletedTask;
    }
}
