using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgentService.Abstractions.Plugins;

public interface IPluginInformation:IPluginInformationBase
{
    void AddPluginService(IServiceCollection services, IConfiguration configuration);
    void AddEndpoints(IEndpointRouteBuilder app);
}