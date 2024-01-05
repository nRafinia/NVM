using Dashboard.Domain.Abstractions;
using Dashboard.Providers;
using Dashboard.Services;
using Microsoft.AspNetCore.Components.Authorization;
using SharedKernel.Shared;

namespace Dashboard;

public class ConfigureServices:IConfigureService
{
    public void AddServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
         
        services.AddScoped<DashboardAuthentication>();
        services.AddScoped<AuthenticationStateProvider, DashboardAuthentication>();
        services.AddScoped<IMainTreeViewService, MainTreeViewService>();
        services.AddSingleton<ICurrentUser, CurrentUserProvider>();

    }
}