using System.Text.Json.Serialization;

namespace Dashboard.Domain.Entities;

public class CredentialBasic : Entity
{
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public Credential Credential { get; }

    [JsonConstructor]
    private CredentialBasic(string id, Credential credential, string userName, string password) : this(credential,
        userName, password)
    {
        Id = id;
    }

    internal CredentialBasic(Credential credential, string userName, string password) : base(IdColumn.New)
    {
        Credential = Guard.Against.Null(credential, nameof(credential));
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
        Password = password;
    }

    public void Update(string userName, string password)
    {
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
        Password = password;
    }
}