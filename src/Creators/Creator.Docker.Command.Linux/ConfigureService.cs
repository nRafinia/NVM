using Microsoft.Extensions.DependencyInjection;

namespace Creator.Docker.Command.Linux;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddScoped<IVirtualMachine, VirtualMachine>();
        return services;
    }

}