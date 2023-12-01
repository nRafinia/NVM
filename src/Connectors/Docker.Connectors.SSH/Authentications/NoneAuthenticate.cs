using Renci.SshNet;

namespace Docker.Connectors.SSH.Authentications;

public class NoneAuthenticate : ISshAuthenticate
{
    private readonly string _userName;

    public NoneAuthenticate(string userName)
    {
        _userName = userName;
    }

    public ConnectionInfo GetCredentials(string host)
    {
        return new ConnectionInfo(host, _userName);
    }
}