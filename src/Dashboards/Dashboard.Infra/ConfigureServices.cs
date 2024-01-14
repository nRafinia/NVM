using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Infra.BackgroundJobs;
using Dashboard.Infra.Repositories;
using Dashboard.Infra.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;
using SharedKernel.Shared;

namespace Dashboard.Infra;

public class ConfigureServices : IConfigureService
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICredentialRepository, CredentialRepository>();
        services.AddSingleton<IDateTime, DateTimeProvider>();
        AddHangfireServices(services, configuration);
    }

    public static void AddInfraApplication(IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions()
        {
            Authorization = new[] { new LocalRequestsOnlyAuthorizationFilter() }
        });
    }

    private static void AddHangfireServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Job");

        services.AddHangfire(setting => setting
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage(connectionString)
        );

        services.AddHangfireServer();
    }
}