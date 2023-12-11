using Microsoft.AspNetCore.Components.Authorization;
using SharedKernel.Shared;

namespace Dashboard;

public class ConfigureServices:IConfigureService
{
    public void AddServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
         
        services.AddScoped<DashboardAuthentication>();
        services.AddScoped<AuthenticationStateProvider, DashboardAuthentication>();
        services.AddHttpContextAccessor();
    }
}