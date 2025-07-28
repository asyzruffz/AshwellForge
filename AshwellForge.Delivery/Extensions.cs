using AshwellForge.Delivery.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AshwellForge.Delivery;

public static class Extensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        ServiceDescriptor[] serviceDescriptors = typeof(Extensions).Assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static WebApplication MapAdminApiEndpoints(this WebApplication app, string apiBaseUri)
    {
        RouteGroupBuilder builder = app.MapGroup(apiBaseUri)
            .AddEndpointFilter<ApiExceptionEndpointFilter>();

        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
