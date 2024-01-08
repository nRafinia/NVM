using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Dashboard.Domain.Enums;

[assembly: InternalsVisibleTo("Dashboard.Infra")]

namespace Dashboard.Domain.Entities;

public class Credential : AuditableEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CredentialType CredentialType { get; private set; }

    public CredentialBasic? BasicCredential { get; private set; }

    [JsonConstructor]
    private Credential(IdColumn id, string name, CredentialType credentialType, string? description) : base(id)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
        CredentialType = credentialType;
    }

    public static Credential None(string name, string? description = default)
        => new (IdColumn.New, name, CredentialType.None, description);
    
    public static Credential Basic(string name, string userName, string password, string? description = default)
    {
        var credential = new Credential(IdColumn.New, name, CredentialType.Basic, description);
        credential.AddBasic(userName, password);
        return credential;
    }

    public void UpdateName(string name, string? description = default)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
    }    
    
    public void UpdateDescription(string description)
    {
        Description = description;
    }

    public CredentialBasic AddBasic(string userName, string password)
    {
        CredentialType = CredentialType.Basic;
        BasicCredential = new CredentialBasic(userName, password);
        return BasicCredential;
    }    
    
    public void RemoveBasic()
    {
        CredentialType = CredentialType.None;
        BasicCredential = null;
    }
}