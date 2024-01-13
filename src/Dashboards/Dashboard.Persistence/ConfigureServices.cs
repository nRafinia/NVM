using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Dashboard.Persistence;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILdapRepository, LdapRepository>();
    }

}