using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Infra.Repositories;
using Dashboard.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;
using SharedKernel.Shared;

namespace Dashboard.Infra;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICredentialRepository, CredentialRepository>();
        services.AddSingleton<IDateTime, DateTimeProvider>();
    }
}