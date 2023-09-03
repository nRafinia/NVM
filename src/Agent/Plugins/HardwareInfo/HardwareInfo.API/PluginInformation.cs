using AgentService.Abstractions.Plugins;
using HardwareInfo.API.Endpoints;
using HardwareInfo.Application;
using HardwareInfo.Domain;
using HardwareInfo.Infra;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareInfo.API;

public class PluginInformation : IPluginInformation
{
    public string Key => "HardwareInfo";
    public string Name => "Hardware information";
    public string Description => string.Empty;

    public void AddPluginService(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services
            .AddDomainServices()
            .AddApplicationServices()
            .AddInfraServices();
    }

    public void AddEndpoints(IEndpointRouteBuilder app)
    {
        app.AddHardwareInformationEndpoints();
    }
}