using AgentService.Abstractions.Plugins;
using FileManager.API.Endpoints.Directories;
using FileManager.API.Endpoints.Files;
using FileManager.API.Endpoints.Paths;
using FileManager.Application;
using FileManager.Domain;
using FileManager.Infra;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.API;

public class PluginInformation : IPluginInformation
{
    public string Name => "File Manager";

    public void AddPluginService(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services
            .AddDomainServices()
            .AddInfraServices()
            .AddApplicationServices();
    }

    public void AddEndpoints(IEndpointRouteBuilder app, string parentTag)
    {
        app
            .AddPathEndpoints(parentTag)
            .AddDirectoryEndpoints(parentTag)
            .AddFileEndpoints(parentTag);
    }
}