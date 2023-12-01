using Renci.SshNet;

namespace Docker.Connectors.SSH.Authentications;

public class BasicAuthenticate : ISshAuthenticate
{
    private readonly string _userName;
    private readonly string _password;

    public BasicAuthenticate(string userName, string password)
    {
        _userName = userName;
        _password = password;
    }

    public ConnectionInfo GetCredentials(string host)
    {
        return new ConnectionInfo(host, _userName, new PasswordAuthenticationMethod(_userName, _password));
    }
}