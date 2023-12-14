
using Docker.DotNet;

namespace Docker.Connectors.API.Authentications;

public class NoneAuthenticate : IApiAuthenticate
{
    private readonly Uri _endpoint;

    public NoneAuthenticate(Uri endpoint)
    {
        _endpoint = endpoint;
    }

    public DockerClientConfiguration GetCredentials()
    {
        return new DockerClientConfiguration(_endpoint);
    }
}