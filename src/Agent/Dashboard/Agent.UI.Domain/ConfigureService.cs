using Agent.UI.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Agent.UI.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<TargetServiceDomain>();

        return services;
    }
}