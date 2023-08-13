using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Contracts;

namespace Shared.Persistence;

public static class ConfigureServices
{
    private const string DatabaseConfigurationName = nameof(DatabaseConfiguration);
    public static IServiceCollection AddSharedPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseConfigurationName)); 
        
        return services;
    }
}