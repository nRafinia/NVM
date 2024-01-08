using Authorizer.Common.Abstractions;

namespace Authorizer.Ldap.Models;

public class LdapConfiguration : IConfiguration
{
    /// <summary>
    /// Represents the port used for a network connection.
    /// LDAP uses port 389.
    /// LDAPS uses port 636.
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Represents the LDAP server's zone (DC=com)
    /// </summary>
    public string Zone { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents the LDAP server's domain (DC=example)
    /// </summary>
    public string Domain { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents the LDAP server's hostname (DC=dc01)
    /// </summary>
    public string Subdomain { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username.
    /// Windows= username
    /// Linux= domain\username
    /// </summary>
    /// <value>The username.</value>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>
    /// The password value.
    /// </value>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Represents the LDAP server used for connecting to the LDAP directory service. (dc01.example.com)
    /// </summary>
    public string LdapServer { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents the LDAP server's base query. (OU=Users,DC=example,DC=com)
    /// </summary>
    public string LdapQueryBase { get; set; } = string.Empty;
}