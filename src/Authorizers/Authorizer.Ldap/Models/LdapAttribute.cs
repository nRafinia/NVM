namespace Authorizer.Ldap.Models;

public class LdapAttribute
{
    /// <summary>
    /// The attribute field to use for tracking user identity across user renames
    /// </summary>
    /// <example>
    /// objectGUID
    /// </example>
    public string UniqueId { get; set; } = "objectGUID";

    /// <summary>
    /// The attribute field to use on the user object.
    /// </summary>
    /// <example>
    /// sAMAccountName
    /// </example>
    public string UserName { get; set; } = "sAMAccountName";
    
    /// <summary>
    /// The attribute field to use when loading the user full name.
    /// </summary>
    /// <example>
    /// displayName
    /// </example>
    public string DisplayName { get; set; } = "displayName";
}