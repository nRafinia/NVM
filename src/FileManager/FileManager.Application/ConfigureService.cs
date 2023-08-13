using System.Reflection;
using FileManager.Application.FileLists;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        Shared.Application.ConfigureServices.AddSharedApplicationServices(services, assemblies);

        services.AddSingleton<IFileListLogic, FileListLogic>();
        
        return services;
    }

}