using Hangfire.Dashboard;

namespace Dashboard.Infra.BackgroundJobs;

public class DashboardNoAuthorizationFilter: IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext dashboardContext)
    {
        return true;
    }
}