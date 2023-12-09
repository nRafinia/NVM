using System.Runtime.InteropServices;

namespace Dashboard.Components.Models;

public class TreeItemData<T>
{
    public string Id { get; init; }
    public TreeItemData<T>? Parent { get; private set; }
    public string Text { get; private set; }
    public bool IsExpanded { get; private set; }

    public T? Data { get; set; }

    private List<TreeItemData<T>> _children= new();
    public IReadOnlyList<TreeItemData<T>> Children => _children.AsReadOnly();
    public bool HasChildren => _children.Any();

    public TreeItemData(string id, string text, T? data, bool isExpanded = false)
    {
        Id = id;
        Text = text;
        Data = data;
        IsExpanded = isExpanded;
    }

    public void Expand()
    {
        IsExpanded = true;
    }
    
    public void Collapse()
    {
        IsExpanded = false;
    }

    public void SetText(string text)
    {
        Text= text;
    }
    
    public void AddChild(params TreeItemData<T>[] children)
    {
        foreach (var child in children)
        {
            child.Parent = this;
            _children.Add(child);
        }
    }
}