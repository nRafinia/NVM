using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Dashboard.Domain.Enums;

[assembly: InternalsVisibleTo("Dashboard.Infra")]

namespace Dashboard.Domain.Entities;

public class Credential(string name, CredentialType credentialType, string? description = default)
    : AuditableEntity(IdColumn.New)
{
    public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    public string? Description { get; private set; } = description;
    public CredentialType CredentialType { get; private set; } = credentialType;

    public CredentialBasic? Basic { get; private set; }

    [JsonConstructor]
    private Credential(string id, string name, CredentialType credentialType, string? description,
        CredentialBasic? basic) : this(name, credentialType, description)
    {
        Id = id;
        Basic = basic;
    }

    public void UpdateName(string name, string? description = default)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
    }

    public CredentialBasic AddBasic(string userName, string password)
    {
        CredentialType = CredentialType.Basic;
        Basic = new CredentialBasic(this, userName, password);
        return Basic;
    }
}