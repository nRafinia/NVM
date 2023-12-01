using Connectors.Docker.Abstractions;
using Renci.SshNet;

namespace Docker.Connectors.SSH.Authentications;

public interface ISshAuthenticate : IAuthenticate
{
    ConnectionInfo GetCredentials(string host);
}