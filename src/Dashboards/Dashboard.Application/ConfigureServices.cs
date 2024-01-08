using Dashboard.Application.Behaviors;
using Dashboard.Application.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Dashboard.Application;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorResult<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(typeof(ConfigureServices).Assembly));

        services.AddScoped<IUserService, UserService>();
    }
}