using Dashboard.Domain.Abstractions;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Infra.Repositories;
using Dashboard.Infra.Services;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Dashboard.Infra;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton<ICredentialRepository, CredentialRepository>();
        services.AddSingleton<IDateTime, DateTimeProvider>();
    }
}