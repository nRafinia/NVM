using AgentService.Abstractions.Plugins;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.API.Endpoints;

namespace Service.API;

public class PluginInformation : IPluginInformation
{
    public static string KeyName => "Service";
    public string Key => KeyName;
    public string Name => "Service";
    public string Description => string.Empty;

    public void AddPluginService(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
    }

    public void AddEndpoints(IEndpointRouteBuilder app)
    {
        app.AddServiceEndpoints();
    }
}