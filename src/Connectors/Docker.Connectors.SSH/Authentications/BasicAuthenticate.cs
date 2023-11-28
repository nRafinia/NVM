namespace Docker.Connectors.SSH.Authentications;

public class BasicAuthenticate : ISshAuthenticate
{
    public string Username { get; }
    public string Password { get; }

    public BasicAuthenticate(string username, string password)
    {
        Username = username;
        Password = password;
    }
}