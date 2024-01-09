using Dashboard.Application.Behaviors;
using Dashboard.Application.Credentials;
using Dashboard.Application.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;
using SharedKernel.Shared;

namespace Dashboard.Application;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorResult<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(typeof(ConfigureServices).Assembly));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICredentialService, CredentialService>();
    }
}