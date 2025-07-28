using Microsoft.AspNetCore.Routing;

namespace AshwellForge.Delivery.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
