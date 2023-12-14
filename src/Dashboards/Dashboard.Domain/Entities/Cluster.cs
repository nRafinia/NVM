namespace Dashboard.Domain.Entities;

public class Cluster(string id, string name, string? description = null) : Entity(id)
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? Description { get; private set; } = description;

    private void Update(string newName, string? newDescription = null)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        Description = newDescription;
    }
}