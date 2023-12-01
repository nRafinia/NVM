using Renci.SshNet;

namespace Docker.Connectors.SSH.Authentications;

public class NoneAuthenticate : ISshAuthenticate
{
    private readonly string _userName;
    private readonly string _host;

    public NoneAuthenticate(string userName, string host)
    {
        _userName = userName;
        _host = host;
    }

    public ConnectionInfo GetCredentials()
    {
        return new ConnectionInfo(_host, _userName);
    }
}