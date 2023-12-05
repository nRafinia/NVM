using Microsoft.AspNetCore.Components;

namespace Dashboard.Components.Models
{
    public delegate RenderFragment CheckboxFragment(bool value, bool indeterminate, Action<bool> valueChanged, bool disabled);
}