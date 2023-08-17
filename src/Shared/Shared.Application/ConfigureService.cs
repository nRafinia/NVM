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
    public static IServiceCollection AddSharedApplicationServices(IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorResult<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));
        AddMapper(services, assemblies);


        return services;
    }

    private static void AddMapper(IServiceCollection services, params Assembly[] assemblies)
    {
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assemblies);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}