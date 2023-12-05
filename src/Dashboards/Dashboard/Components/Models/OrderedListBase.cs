using Microsoft.AspNetCore.Components;

namespace Dashboard.Components.Models
{
    public class OrderedListItemBase<T> : ComponentBase
    {
        [CascadingParameter] 
        protected TreeView<T>? TreeView { get; set; }
        
        protected string? Class => TreeView?.ListClass;
        [Parameter] 
        public int Level { get; set; }
        [Parameter] 
        public bool Collapsed { get; set; }
    }
}