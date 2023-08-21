using FileManager.Application.Abstraction;
using FileManager.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddSingleton<IFiles, FileService>();
        
        return services;
    }

}