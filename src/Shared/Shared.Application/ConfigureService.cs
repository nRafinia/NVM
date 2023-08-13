using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Shared.Behaviors;

namespace Shared.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddSharedApplicationServices(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorResult<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        AddMapper(services, assembly);


        return services;
    }

    private static void AddMapper(IServiceCollection services, Assembly assembly)
    {
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}