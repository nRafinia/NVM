
using Docker.DotNet;

namespace Docker.Connectors.API.Authentications;

public class NoneAuthenticate : IApiAuthenticate
{
    public Credentials? GetCredentials()
    {
        return default;
    }
}