using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Infra.Handlers;
using Agent.UI.Infra.Interfaces;
using Agent.UI.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Agent.UI.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ServiceEndpointHandler>();

        services.AddTransient<IService, ServiceImp>();
        services.AddTransient<IHardwareInformation, HardwareInformationImp>();

        AddRefitServices(services);

        return services;
    }

    private static void AddRefitServices(IServiceCollection services)
    {
        services
            .AddRefitClient<IServiceEndpoint>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(ServiceEndpointHandler.FakeBaseAddress))
            .AddHttpMessageHandler<ServiceEndpointHandler>();
        services
            .AddRefitClient<IHardwareInformationEndpoint>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(ServiceEndpointHandler.FakeBaseAddress))
            .AddHttpMessageHandler<ServiceEndpointHandler>();
    }
}