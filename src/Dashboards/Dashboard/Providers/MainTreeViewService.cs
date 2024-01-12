using Dashboard.Components.Models;
using Dashboard.Const;

namespace Dashboard.Providers;

public class MainTreeViewService : IMainTreeViewService
{
    private readonly List<TreeItemData<MainTreeModel>> _treeViewItems = new();

    private const string Dashboard = "Dashboard";
    private const string Credential = "Credential";
    private const string Authentication = "Authentication";
    private const string User = "Users";
    private const string Ldap = "LDAP";

    public MainTreeViewService()
    {
        AddDefaultDashboards();
    }

    public List<TreeItemData<MainTreeModel>> GetTreeViewItems => _treeViewItems;

    private TreeItemData<MainTreeModel>? _selected;
    public TreeItemData<MainTreeModel>? Selected { 
        get=>_selected;
        set
        {
            _selected = value; 
            OnChange?.Invoke();
        }
    }

    public Action? OnChange { get; set; }
    
    private void AddDefaultDashboards()
    {
        var mainDashboard = new TreeItemData<MainTreeModel>(
            Dashboard,
            new MainTreeModel(DashboardIds.Main));
        _treeViewItems.Add(mainDashboard);

        var credentialDashboard = new TreeItemData<MainTreeModel>(
            Credential,
            new MainTreeModel(DashboardIds.Credential));
        _treeViewItems.Add(credentialDashboard);
        
        var authentications = new TreeItemData<MainTreeModel>(
            Authentication,
            new MainTreeModel(DashboardIds.Authentication));
        _treeViewItems.Add(authentications);        
        var userDashboard = new TreeItemData<MainTreeModel>(
            User,
            new MainTreeModel(DashboardIds.User));
        authentications.AddChild(userDashboard);
        var ldapDashboard = new TreeItemData<MainTreeModel>(
            Ldap,
            new MainTreeModel(DashboardIds.LDAP));
        authentications.AddChild(ldapDashboard);
    }
}