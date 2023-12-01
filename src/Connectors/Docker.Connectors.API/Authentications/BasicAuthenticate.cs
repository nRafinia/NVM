using Docker.DotNet;
using Docker.DotNet.BasicAuth;

namespace Docker.Connectors.API.Authentications;

public class BasicAuthenticate : IApiAuthenticate
{
    public string Username { get; }
    public string Password { get; }

    public BasicAuthenticate(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public Credentials? GetCredentials()
    {
        return new BasicAuthCredentials(Username, Password);
    }
}