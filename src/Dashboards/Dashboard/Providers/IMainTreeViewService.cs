using Dashboard.Components.Models;

namespace Dashboard.Providers;

public interface IMainTreeViewService
{
    List<TreeItemData<MainTreeModel>> GetTreeViewItems { get; }
    TreeItemData<MainTreeModel>? Selected { get; set; }
    Action? OnChange { get; set; }
}