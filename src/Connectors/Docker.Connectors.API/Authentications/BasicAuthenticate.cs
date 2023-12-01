using Docker.DotNet;
using Docker.DotNet.BasicAuth;

namespace Docker.Connectors.API.Authentications;

public class BasicAuthenticate : IApiAuthenticate
{
    private readonly string _username;
    private readonly string _password;
    private readonly Uri _endpoint;

    public BasicAuthenticate(Uri endpoint, string username, string password)
    {
        _endpoint = endpoint;
        _username = username;
        _password = password;
    }

    public DockerClientConfiguration GetCredentials()
    {
        return new DockerClientConfiguration(_endpoint, new BasicAuthCredentials(_username, _password));
    }
}