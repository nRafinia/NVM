using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel;

public interface IConfigureService
{
    void AddSharedDomainServices(IServiceCollection services);
}