using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Shared;

public static class RegisterServiceHelper
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var servicesToRegister = Common.GetImplementedInterfaceOf<IConfigureService>(assemblies).ToList();
        servicesToRegister.ForEach(x => x?.AddServices(services, configuration));
    }
}