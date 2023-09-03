using Microsoft.AspNetCore.Routing;
using Service.API.Models;
using Shared.Presentation.Extensions;

namespace Service.API.Endpoints;

public static class ServiceEndpoints
{

    public static IEndpointRouteBuilder AddServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapHttpGet<GetPluginsRequest>("/");

        return app;
    }
}