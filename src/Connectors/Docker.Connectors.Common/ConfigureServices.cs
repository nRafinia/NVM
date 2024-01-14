using System.ComponentModel;
using Connectors.Docker.Images;
using Connectors.Docker.Networks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Connectors.Docker;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContainer, Container>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<INetworkService, NetworkService>();
    }
}