using System.DirectoryServices.Protocols;
using Authorizer.Common.Abstractions;
using SharedKernel.ValueObjects;

namespace Authorizer.Ldap.Models;

public class LdapConfiguration(string hostname, IdColumn credentialId, string baseDn) : IConfiguration
{
    /// <summary>
    /// Represents the port used for a network connection.
    /// LDAP: 389
    /// LDAPS (SSL): 636
    /// </summary>
    public int Port { get; set; } = 389;

    /// <summary>
    /// Use secure connection (LDAPS)
    /// </summary>
    public bool UseSecure { get; set; } = false;

    /// <summary>
    /// Hostname of the server running LDAP (IP or DNS name)
    /// <example>ldap.example.com</example>
    /// </summary>
    public string HostName { get; set; } = hostname;

    /// <summary>
    /// The credential used for authentication and authorization.
    /// For Microsoft Active Directory, provide the username.
    /// For other systems, provide the domain\username.
    /// Example: user@domain.name or cn=user,dc=domain,dc=name
    /// </summary>
    public IdColumn CredentialId { get; set; } = credentialId;
    
    /// <summary>
    /// Root node in LDAP from which to search for users and groups
    /// <example>cn=users,dc=example,dc=com</example>
    /// </summary>
    public string BaseDn { get; set; } = baseDn;

    /// <summary>
    /// The filter to use when searching user objects.
    /// </summary>
    /// <example>
    /// (&(objectCategory=Person)(sAMAccountName=*))
    /// </example>
    public string FilterQuery { get; set; } = "(&(objectCategory=Person)(sAMAccountName=*))";

    /// <summary>
    /// The LDAP attributes.
    /// </summary>
    public LdapAttribute Attributes { get; set; } = new();

    /// <summary>
    /// The LDAP search scope.
    /// </summary>
    /// <example>
    /// Subtree, OneLevel, Base
    /// </example>
    public SearchScope Scope { get; set; } = SearchScope.Subtree;

    /// <summary>
    /// on Windows the authentication type is Negotiate, so there is no need to prepend
    /// AD user login with domain. On other platforms at the moment only
    /// Basic authentication is supported
    /// </summary>
    /// <example>
    /// Negotiate, Basic
    /// </example>
    public AuthType AuthenticationType { get; set; } = AuthType.Negotiate;

    /// <summary>
    /// the default one is v2 (at least in that version), and it is unknown if v3
    /// is actually needed, but at least Synology LDAP works only with v3,
    /// and since our Exchange doesn't complain, let it be v3
    /// </summary>
    public int ProtocolVersion { get; set; } = 3;
}