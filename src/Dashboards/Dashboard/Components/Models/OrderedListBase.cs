using Microsoft.AspNetCore.Components;

namespace Dashboard.Components.Models
{
    public class OrderedListBase<T> : ComponentBase
    {
        [CascadingParameter] 
        protected TreeView<T>? TreeView { get; set; }
        
        protected RenderFragment<ItemContent<T>>? ItemTemplate => TreeView?.ItemTemplate;
        protected Func<IEnumerable<T>, IEnumerable<T>?>? FilterBy => TreeView?.FilterBy;
        protected Func<IEnumerable<T>, IEnumerable<T>?>? SortBy => TreeView?.SortBy;
        protected string? Class => TreeView?.ListClass;
        [Parameter] 
        public int Level { get; set; }
        [Parameter] 
        public bool Collapsed { get; set; }
    }
}