using System.Text.Json.Serialization;

namespace Dashboard.Domain.Entities;

public class CredentialBasic : Entity
{
    public string UserName { get; private set; }
    public string Password { get; private set; }

    [JsonConstructor]
    private CredentialBasic(IdColumn id, string userName, string password) : base(IdColumn.New)
    {
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
        Password = password;
    }

    internal CredentialBasic(string userName, string password) : this(IdColumn.New, userName, password)
    {
    }

    public void Update(string userName, string password)
    {
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
        Password = password;
    }
    
    public void UpdatePassword(string password)
    {
        Password = password;
    }
    
    public void UpdateUserName(string userName)
    {
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
    }
}