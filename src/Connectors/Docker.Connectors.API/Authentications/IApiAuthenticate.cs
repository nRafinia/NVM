using Connectors.Docker.Abstractions;
using Docker.DotNet;

namespace Docker.Connectors.API.Authentications;

public interface IApiAuthenticate : IAuthenticate
{
    DockerClientConfiguration GetCredentials();
}