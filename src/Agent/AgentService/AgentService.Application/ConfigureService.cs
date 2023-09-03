using System.Reflection;
using AgentService.Abstractions.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace AgentService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        Shared.Application.ConfigureServices.AddSharedApplicationServices(services, assemblies);

        services.AddScoped<IPlugins, Plugins.Plugins>();

        return services;
    }
}