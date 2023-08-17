using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        Shared.Domain.ConfigureServices.AddSharedDomainServices(services);

        return services;
    }

}