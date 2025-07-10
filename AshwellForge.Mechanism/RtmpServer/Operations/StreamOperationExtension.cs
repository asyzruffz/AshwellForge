using AshwellForge.Core.Data;
using AshwellForge.Mechanism.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AshwellForge.Mechanism.RtmpServer.Operations;

internal static class StreamOperationExtension
{
    public static IServiceCollection AddStreamOperations(this IServiceCollection services)
    {
        services.AddScoped<IOperationHandler<GetStreamsOperation, GetStreamsResponse>, GetStreamsOperationHandler>();
        services.AddScoped<IOperationHandler<DeleteStreamOperation>, DeleteStreamOperationHandler>();
        return services;
    }
}
