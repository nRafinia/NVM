using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Domain.Entities.Users;
using Dashboard.Services;
using Mapster;
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
        services.AddSingleton<ICurrentUser, CurrentUserService>();
        
        ConfigureMapping();
    }

    private static void ConfigureMapping()
    {
        TypeAdapterConfig<User, UserSession>
            .NewConfig()
            .Map(d => d.UserName, s => s.UserName)
            .Map(d => d.UserId, s => s.Id);
    }
}