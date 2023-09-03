using AgentService.Abstractions.Plugins;
using Microsoft.AspNetCore.Http;
using Service.API.Models;
using Shared.Domain.Base.Results;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace Service.API.Endpoints;

public class GetPluginsRequestHandler : IHttpRequestHandler<GetPluginsRequest>
{
    private readonly IPlugins _plugins;

    public GetPluginsRequestHandler(IPlugins plugins)
    {
        _plugins = plugins;
    }

    public Task<IResult> Handle(GetPluginsRequest request, CancellationToken cancellationToken)
    {
        var serviceKey = PluginInformation.KeyName;
        var plugins = _plugins.GetPlugins;
        var response = plugins
            .Where(p => p.Key != serviceKey)
            .Select(p => new GetPluginsResponse(p.Key, p.Name, p.Description));
        return Task.FromResult(Result.Success(response).GetHttpResponse());
    }
}