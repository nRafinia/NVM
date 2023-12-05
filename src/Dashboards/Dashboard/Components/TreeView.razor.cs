using Dashboard.Components.Models;
using Microsoft.AspNetCore.Components;

namespace Dashboard.Components;

public partial class TreeView<T> : TreeViewBase<T>
{
    /// <summary>
    /// The items to be displayed in the <see cref="TreeView"/>
    /// </summary>
    [Parameter]
    public List<T>? Items { get; set; }

    [Parameter]
    public Func<T, List<T>>? GetChildren { get; set; }

    /// <summary>
    /// If this parameter is set, the TreeView will be populate based on the result of this method. Only works in conjunction with <see cref="GetChildren" />, not with <see cref="GetParent"/>.
    /// </summary>
    [Parameter]
    public Func<T, bool>? HasChildren { get; set; }

    private bool _hasChildrenInitialized;

    protected override void OnParametersSet()
    {
        if (!_hasChildrenInitialized)
        {
            if (HasChildren != null)
            {
                InitiallyCollapsed = true; // Collapse all if we want lazy loading, else we'll load everything at first
            }
            else
            {
                // In general case, we stay with default HasChildren behaviour
                HasChildren = item =>
                {
                    var childItem = GetChildren?.Invoke(item);
                    return childItem != null && childItem.Any();
                };
            }

            _hasChildrenInitialized = true;
        }

        base.OnParametersSet();
    }
}