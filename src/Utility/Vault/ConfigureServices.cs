using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Vault;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFileUtility, FileUtility>();
        services.AddSingleton<IVaultManager, VaultManager>();
    }
}