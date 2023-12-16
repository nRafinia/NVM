using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Shared;

namespace Vault;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IFileUtility, FileUtility>();
        services.AddSingleton<ICredentialManager, CredentialManager>();
    }
}