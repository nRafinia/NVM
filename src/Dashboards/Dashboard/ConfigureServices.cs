using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Providers;
using Dashboard.Services;
using Microsoft.AspNetCore.Components.Authorization;
using SharedKernel.Abstractions;
using SharedKernel.Shared;

namespace Dashboard;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddSweetAlert2();

        services.AddScoped<DashboardAuthentication>();
        services.AddScoped<AuthenticationStateProvider, DashboardAuthentication>();
        services.AddScoped<IMainTreeViewService, MainTreeViewService>();
        services.AddSingleton<ICurrentUser, CurrentUserProvider>();
    }
}