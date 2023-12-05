using Dashboard.Components.Models;
using Microsoft.AspNetCore.Components;

namespace Dashboard.Components
{
    public sealed partial class TreeItem<T> : TreeItemBase, IDisposable
    {
        [Parameter] public T Item { get; set; }
        private bool Selected { get; set; }
        private bool Indeterminate { get; set; }

        private void SelectedChanged(bool newSelected)
        {
            if (_disposed)
            {
                return;
            }

            if (newSelected == Selected && !Indeterminate)
            {
                return;
            }

            Indeterminate = false;
            Selected = newSelected;
            OnSelectedChanged?.Invoke(Selected);
            Parent?.ReevaluateSelected();
            TreeView?.UpdateSelection(Item, Selected, Indeterminate);
            InvokeAsync(StateHasChangedIfNotDisposed);
        }

        private void SelectedAndIndeterminateChanged(bool newSelected, bool newIndeterminate)
        {
            if (_disposed)
            {
                return;
            }

            if (newSelected == Selected && newIndeterminate == Indeterminate)
            {
                return;
            }

            Indeterminate = newIndeterminate;
            Selected = newSelected;
            OnSelectedChanged?.Invoke(Selected);
            Parent?.ReevaluateSelected();
            TreeView?.UpdateSelection(Item, Selected, Indeterminate);
            InvokeAsync(StateHasChangedIfNotDisposed);
        }

        private void IndeterminateChanged(bool indeterminate)
        {
            if (_disposed)
            {
                return;
            }

            Indeterminate = indeterminate;
            Parent?.ReevaluateSelected();
            TreeView?.UpdateSelection(Item, Selected, Indeterminate);
            InvokeAsync(StateHasChangedIfNotDisposed);
        }

        private event Action<bool>? OnSelectedChanged;
        
        [CascadingParameter] 
        private TreeViewBase<T>? TreeView { get; set; }

        [CascadingParameter] 
        private TreeItem<T>? Parent { get; set; }
        
        private HashSet<TreeItem<T>> _children = new HashSet<TreeItem<T>>();
        private string? Class => TreeView?.ItemClass;
        
        [Parameter] 
        public EventCallback<bool>? CollapseHasChanged { get; set; }

        [Parameter] 
        public bool LoadingChild { get; set; }

        private bool _disabled;
        public bool Disabled => TreeView?.Disabled == true || Parent?.Disabled == true || _disabled;

        protected override void OnInitialized()
        {
            CollapsedChanged = CollapseChanged;
            if (TreeView?.InitiallyCollapsed == true)
            {
                Collapsed = true;
            }

            if (!Disabled)
            {
                var thisShouldBeSelected = Parent?.Selected == true || TreeView?.SelectedItems?.Contains(Item) == true;
                SelectedChanged(thisShouldBeSelected);
            }

            base.OnInitialized();
        }

        private async void CollapseChanged()
        {
            if (CollapseHasChanged is null)
            {
                return;
            }

            await CollapseHasChanged.Value.InvokeAsync(Collapsed);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (_disposed)
            {
                return;
            }

            if (firstRender)
            {
                if (TreeView?.SelectedItems != null && TreeView.SelectedItems.Contains(Item))
                {
                    SelectedChanged(true);
                }

                Parent?.ReevaluateSelected();
            }

            base.OnAfterRender(firstRender);
        }

        protected override void OnParametersSet()
        {
            if (Parent != null)
            {
                if (Parent._children.Add(this))
                {
                    Parent.OnSelectedChanged += ReactOnSelectedChanged;
                }
            }

            if (TreeView?.ItemDisabled != null)
            {
                _disabled = TreeView.ItemDisabled.Invoke(Item);
            }

            base.OnParametersSet();
        }

        private void ReevaluateSelected()
        {
            if (_disposed)
            {
                return;
            }

            if (!_children.Any())
            {
                return;
            }

            bool? state = null;
            // The state of indeterminate needs to be true if
            // - at least one child is indeterminate, OR
            // - at least two children differ in state
            var indeterminate = _children.Any(x => x.Indeterminate) ||
                                (_children.Any(x => x.Selected) && _children.Any(x => !x.Selected));
            if (_children.All(x => x is { Selected: true, Indeterminate: false }))
            {
                state = true;
            }
            else if (_children.All(x => x is { Selected: false, Indeterminate: false }))
            {
                state = false;
            }

            if (state == null)
            {
                IndeterminateChanged(indeterminate);
            }
            else
            {
                SelectedAndIndeterminateChanged(state.Value, indeterminate);
            }
        }

        private RenderFragment<ItemContent<T>>? ItemTemplate => TreeView?.ItemTemplate;
        private CheckboxFragment CheckboxTemplate => TreeView?.CheckboxTemplate;
        private bool AllowSelection => TreeView?.AllowSelection??false;
        private bool _disposed;

        private void StateHasChangedIfNotDisposed()
        {
            if (_disposed)
            {
                return;
            }
            try
            {
                StateHasChanged();
            }
            catch
            {
                // ignored
            }
        }

        private void ReactOnSelectedChanged(bool newValue)
        {
            if (_disposed)
            {
                return;
            }

            if (Disabled)
            {
                return;
            }

            SelectedChanged(newValue);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            if (Parent == null)
            {
                return;
            }
            Parent.OnSelectedChanged -= ReactOnSelectedChanged;
            Parent._children.Remove(this);
            Parent.ReevaluateSelected();
        }

        private RenderFragment LoadChildrenTemplate => (TreeView as TreeViewAsync<T>)?.LoadingTemplate;
    }
}