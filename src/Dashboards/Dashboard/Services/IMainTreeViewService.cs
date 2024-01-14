using Dashboard.Components.Models;
using Dashboard.Models;

namespace Dashboard.Services;

public interface IMainTreeViewService
{
    List<TreeItemData<MainTreeModel>> GetTreeViewItems { get; }
    TreeItemData<MainTreeModel>? Selected { get; set; }
    Action? OnChange { get; set; }
}