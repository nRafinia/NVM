using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.HttpEndpointHandlers;

namespace Shared.Presentation.Extensions;

public static class HttpHandlerExt
{
    public static RouteHandlerBuilder MapHttpGet<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken) =>
            await mediator.Send(request, cancellationToken));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoint;
    }

    public static RouteHandlerBuilder MapHttpPost<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPost(template,
            async (IMediator mediator, [FromBody] TRequest request, CancellationToken cancellationToken) =>
            await mediator.Send(request, cancellationToken));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoint;
    }

    public static RouteHandlerBuilder MapHttpPut<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPut(template,
            async (IMediator mediator, [FromBody] TRequest request, CancellationToken cancellationToken) =>
            await mediator.Send(request, cancellationToken));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoint;
    }

    public static RouteHandlerBuilder MapHttpDelete<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapDelete(template,
            async (IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken) =>
            await mediator.Send(request, cancellationToken));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoint;
    }

    private static void SetConfiguration(IEndpointConventionBuilder endpoint, MapHttpConfiguration configuration)
    {
        if (configuration.AllowAnonymous)
        {
            endpoint.AllowAnonymous();
        }
        else
        {
            if (configuration.Policy is null)
            {
                endpoint.RequireAuthorization();
            }
            else
            {
                configuration.Policy.Apply(endpoint);
            }
        }

        if (configuration.Description is not null)
        {
            endpoint.WithDescription(configuration.Description);
        }

        if (configuration.Name is not null)
        {
            endpoint.WithName(configuration.Name);
        }

        if (configuration.Summary is not null)
        {
            endpoint.WithSummary(configuration.Summary);
        }

        endpoint.WithOpenApi();
    }
}