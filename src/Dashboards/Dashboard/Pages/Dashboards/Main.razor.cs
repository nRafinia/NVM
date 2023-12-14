namespace Dashboard.Pages.Dashboards;

public partial class Main :DashboardPage
{
    public readonly Guid PageId = new ("a0b1c2d3-e4f5-6a7b-8c9d-0e1f2a3b4c5d");
    public readonly string PageTitle = "NVM";
    public override string Description => "NVM";

    protected override void OnParametersSet()
    {
        base.Title = PageTitle;
    }
}