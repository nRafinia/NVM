using FileManager.Application.FileLists;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileLogic, FileLogic>();
        
        return services;
    }

}