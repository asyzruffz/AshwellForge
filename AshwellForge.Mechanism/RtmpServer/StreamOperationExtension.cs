using AshwellForge.Mechanism.Core;
using AshwellForge.Mechanism.RtmpServer.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer;

internal static class StreamOperationExtension
{
    public static IServiceCollection AddStreamOperations(this IServiceCollection services)
    {
        services.AddScoped<IOperationHandler<GetStreamsOperation, GetStreamsResponse>, GetStreamsOperationHandler>();
        services.AddScoped<IOperationHandler<DeleteStreamOperation>, DeleteStreamOperationHandler>();
        return services;
    }
}
