using Microsoft.Extensions.DependencyInjection;

namespace AgentService.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        Shared.Domain.ConfigureServices.AddSharedDomainServices(services);

        return services;
    }

}