using Authorizer.Common.Models;
using Authorizer.Local.Domain.Entities;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Authorizer.Local;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        ConfigMapping();
    }

    private static void ConfigMapping()
    {
        TypeAdapterConfig<User, UserInfo>
            .NewConfig()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.Name, s => s.DisplayName)
            .Map(d => d.UserName, s => s.UserName)
            .MapToConstructor(true);
    }
}