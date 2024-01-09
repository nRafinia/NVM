using System.DirectoryServices.Protocols;
using Authorizer.Ldap.Models;

namespace Authorizer.Ldap.Helpers;

internal static class LdapTools
{
    public static SearchResponse Search(LdapConfiguration configuration, IEnumerable<string> attributes)
    {
        var connection = new LdapConnection(new LdapDirectoryIdentifier(configuration.HostName, configuration.Port))
        {
            AuthType = configuration.AuthenticationType,
            Credential = new(configuration.Username, configuration.Password)
        };
         
        // the default one is v2 (at least in that version), and it is unknown if v3
        // is actually needed, but at least Synology LDAP works only with v3,
        // and since our Exchange doesn't complain, let it be v3
        connection.SessionOptions.ProtocolVersion = configuration.ProtocolVersion;

        // this is for connecting via LDAPS (636 port). It should be working,
        // according to https://github.com/dotnet/runtime/issues/43890,
        // but it doesn't (at least with Synology DSM LDAP), although perhaps
        // for a different reason
        connection.SessionOptions.SecureSocketLayer = configuration.UseSecure;

        connection.Bind();

        var request = new SearchRequest(
            configuration.BaseDn, 
            configuration.FilterQuery, 
            configuration.Scope, 
            attributes.ToArray());

        return (SearchResponse)connection.SendRequest(request);
    }
    
    
}