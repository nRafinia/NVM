namespace Dashboard.Domain.Entities;

public class User(string id, string name, string userName, string password) : AuditableEntity(id)
{
    public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string UserName { get; } = Guard.Against.NullOrEmpty(userName, nameof(userName));
    public string Password { get; private set; } = Guard.Against.NullOrEmpty(password, nameof(password));
}