using Hardware.Info;
using HardwareInfo.Application.Abstraction.Interfaces;
using HardwareInfo.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareInfo.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddSingleton<IHardwareInformation, HardwareInformation>();
        services.AddSingleton<IHardwareInfo>(_ => new Hardware.Info.HardwareInfo());
        
        return services;
    }

}