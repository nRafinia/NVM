using Microsoft.Extensions.DependencyInjection;

namespace Shared.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedDomainServices(IServiceCollection services)
    {
        return services;
    }

}