using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

}