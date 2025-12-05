using AshwellForge.Core.RtmpServer;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Net;

namespace AshwellForge.Mechanism.RtmpServer.Services;

internal class StreamSessionService : IStreamSessionService, IAsyncDisposable
{
    readonly IServiceScopeFactory scopeFactory;
    readonly ConcurrentDictionary<Guid, CancellationTokenSource> sessions = new();
    readonly IServerEventDispatcher eventDispatcher;

    public StreamSessionService(IServiceScopeFactory serviceScopeFactory, IServerEventDispatcher serverEventDispatcher)
    {
        scopeFactory = serviceScopeFactory;
        eventDispatcher = serverEventDispatcher;
    }

    public async Task<Guid> SpawnStreamSession()
    {
        var id = Guid.NewGuid();
        var cts = new CancellationTokenSource();
        if (!sessions.TryAdd(id, cts))
        {
            await StopStreamSession(id);
            sessions[id] = cts;
        }

        _ = Task.Run(async () =>
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            var streamSession = scope.ServiceProvider
                .GetRequiredService<IStreamSession>();

            try
            {
                await eventDispatcher.StreamSessionStartedAsync(id);
                await streamSession.RunAsync(new IPEndPoint(IPAddress.Any, 1935), cts.Token);
            }
            finally
            {
                sessions.TryRemove(id, out _);
                await eventDispatcher.StreamSessionStoppedAsync(id);
            }
        });

        return id;
    }

    public async Task StopStreamSession(Guid id)
    {
        if (sessions.TryRemove(id, out var cts))
        {
            await StopStreamSessionInternal(id, cts);
        }
    }

    async Task StopStreamSessionInternal(Guid id, CancellationTokenSource cts)
    {
        cts.Cancel();
        await eventDispatcher.StreamSessionStoppedAsync(id);
    }

    public async ValueTask DisposeAsync()
    {
        var stoppingTasks = sessions.Select(entry =>
            StopStreamSessionInternal(entry.Key, entry.Value));

        await Task.WhenAll(stoppingTasks);
        await eventDispatcher.ServerStoppedAsync();
    }
}
