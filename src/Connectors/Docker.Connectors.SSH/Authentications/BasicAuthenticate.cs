using Renci.SshNet;

namespace Docker.Connectors.SSH.Authentications;

public class BasicAuthenticate : ISshAuthenticate
{
    private readonly string _userName;
    private readonly string _password;
    private readonly string _host;

    public BasicAuthenticate(string userName, string password, string host)
    {
        _userName = userName;
        _password = password;
        _host = host;
    }

    public ConnectionInfo GetCredentials()
    {
        return new ConnectionInfo(_host, _userName, new PasswordAuthenticationMethod(_userName, _password));
    }
}