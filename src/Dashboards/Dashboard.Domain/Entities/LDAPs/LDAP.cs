using Dashboard.Domain.Entities.LDAPs.Enums;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Domain.Entities.LDAPs;

public class LDAP(
    string name,
    int port,
    bool useSecure,
    string hostName,
    IdColumn credentialId,
    string baseDn,
    string filterQuery = "(&(objectCategory=Person)(sAMAccountName=*))",
    SearchScope scope = SearchScope.Subtree,
    AuthType authenticationType = AuthType.Negotiate,
    int protocolVersion = 3) : Entity(IdColumn.New)
{
    public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    /// <summary>
    /// Represents the port used for a network connection.
    /// LDAP: 389
    /// LDAPS (SSL): 636
    /// </summary>
    public int Port { get; private set; } = Guard.Against.NegativeOrZero(port, nameof(port));

    /// <summary>
    /// Use secure connection (LDAPS)
    /// </summary>
    public bool UseSecure { get; private set; } = useSecure;

    /// <summary>
    /// Hostname of the server running LDAP (IP or DNS name)
    /// <example>ldap.example.com</example>
    /// </summary>
    public string HostName { get; private set; } = Guard.Against.NullOrWhiteSpace(hostName, nameof(hostName));

    /// <summary>
    /// The credential used for authentication and authorization.
    /// For Microsoft Active Directory, provide the username.
    /// For other systems, provide the domain\username.
    /// Example: user@domain.name or cn=user,dc=domain,dc=name
    /// </summary>
    public IdColumn CredentialId { get; private set; } = Guard.Against.IdColumn(credentialId, nameof(credentialId));

    /// <summary>
    /// Root node in LDAP from which to search for users and groups
    /// <example>cn=users,dc=example,dc=com</example>
    /// </summary>
    public string BaseDn { get; private set; } = Guard.Against.NullOrEmpty(baseDn, nameof(baseDn));

    /// <summary>
    /// The filter to use when searching user objects.
    /// </summary>
    /// <example>
    /// (&(objectCategory=Person)(sAMAccountName=*))
    /// </example>
    public string FilterQuery { get; private set; } = filterQuery;

    /// <summary>
    /// The LDAP search scope.
    /// </summary>
    /// <example>
    /// Subtree, OneLevel, Base
    /// </example>
    public SearchScope Scope { get; private set; } = scope;

    /// <summary>
    /// on Windows the authentication type is Negotiate, so there is no need to prepend
    /// AD user login with domain. On other platforms at the moment only
    /// Basic authentication is supported
    /// </summary>
    /// <example>
    /// Negotiate, Basic
    /// </example>
    public AuthType AuthenticationType { get; private set; } = authenticationType;

    /// <summary>
    /// the default one is v2 (at least in that version), and it is unknown if v3
    /// is actually needed, but at least Synology LDAP works only with v3,
    /// and since our Exchange doesn't complain, let it be v3
    /// </summary>
    public int ProtocolVersion { get; private set; } = protocolVersion;

    public LdapAttribute Attributes { get; set; } = new();
    public IdColumn AttributesId { get; set; }

    public void Update(string name, int port, bool useSecure, string hostName, IdColumn credentialId, string baseDn,
        string filterQuery, SearchScope scope, AuthType authenticationType, int protocolVersion)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Port = Guard.Against.NegativeOrZero(port, nameof(port));
        UseSecure = useSecure;
        HostName = Guard.Against.NullOrWhiteSpace(hostName, nameof(hostName));
        CredentialId = Guard.Against.IdColumn(credentialId, nameof(credentialId));
        BaseDn = Guard.Against.NullOrEmpty(baseDn, nameof(baseDn));
        FilterQuery = filterQuery;
        Scope = scope;
        AuthenticationType = authenticationType;
        ProtocolVersion = protocolVersion;
    }
}