using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Shared;

public static class RegisterServiceHelper
{
    public static void RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        var servicesToRegister = Common.GetImplementedInterfaceOf<IConfigureService>(assemblies).ToList();
        servicesToRegister.ForEach(x => x.AddServices(services));
    }
}