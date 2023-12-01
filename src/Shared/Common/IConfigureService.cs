using Microsoft.Extensions.DependencyInjection;

namespace Common;

public interface IConfigureService
{
    void AddSharedDomainServices(IServiceCollection services);
}