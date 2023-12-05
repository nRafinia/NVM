using Microsoft.AspNetCore.Components;

namespace Dashboard.Components.Models
{
    public class ListItemBase : ComponentBase
    {
        [Parameter] 
        public int Level { get; set; }
        [Parameter] 
        public RenderFragment<bool>? ChildContent { get; set; }
        [Parameter] 
        public bool HasChildren { get; set; }
        internal bool Collapsed { get; set; }
        internal Action? CollapsedChanged { get; set; }
    }
}