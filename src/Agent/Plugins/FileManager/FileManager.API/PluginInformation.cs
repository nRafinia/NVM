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
    public string Key => "FileManager";
    public string Name => "File Manager";
    public string Description => string.Empty;

    public void AddPluginService(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services
            .AddDomainServices()
            .AddInfraServices()
            .AddApplicationServices();
    }

    public void AddEndpoints(IEndpointRouteBuilder app)
    {
        app
            .AddPathEndpoints()
            .AddDirectoryEndpoints()
            .AddFileEndpoints();
    }
}