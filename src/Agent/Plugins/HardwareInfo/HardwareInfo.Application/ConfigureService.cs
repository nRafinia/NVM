using HardwareInfo.Application.HardwareInformation;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareInfo.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IHardwareInformationLogic, HardwareInformationLogic>();
        
        return services;
    }

}