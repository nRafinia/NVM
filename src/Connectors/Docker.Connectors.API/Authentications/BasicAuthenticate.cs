using Docker.DotNet;
using Docker.DotNet.BasicAuth;

namespace Docker.Connectors.API.Authentications;

public class BasicAuthenticate : IApiAuthenticate
{
    private readonly string _username;
    private readonly string _password;

    public BasicAuthenticate(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public Credentials GetCredentials()
    {
        return new BasicAuthCredentials(_username, _password);
    }
}