using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Abstractions.Interfaces;
using Shared.Infra.Http;
using Shared.Infra.Services;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Shared.Infra;

public static class ConfigureServices
{
    private const string RedisConfigurationName = nameof(RedisConfiguration);

    public static IServiceCollection AddSharedInfraServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<HttpLoggingHandler>();

        AddCacheMemory(services, configuration);

        return services;
    }

    private static void AddCacheMemory(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(RedisConfigurationName);
        if (!section.Exists())
        {
            return;
        }

        var redisConfiguration = section.Get<RedisConfiguration>()!;
        services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration);
        services.AddSingleton<ICache, CacheService>();
    }
}