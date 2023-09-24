using Agent.UI.Application.FileManager;
using Agent.UI.Application.HardwareInfo;
using Agent.UI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Agent.UI.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IServiceLogic, ServiceLogic>();
        services.AddTransient<IHardwareInformationLogic, HardwareInformationLogic>();
        services.AddTransient<IFileManagerLogic, FileManagerLogic>();

        return services;
    }
}