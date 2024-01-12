using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Domain.Entities.LDAPs;

public class LdapAttribute(
    string uniqueId = "objectGUID",
    string userName = "sAMAccountName",
    string displayName = "displayName") : Entity(IdColumn.New)
{
    /// <summary>
    /// The attribute field to use for tracking user identity across user renames
    /// </summary>
    /// <example>
    /// objectGUID
    /// </example>
    public string UniqueId { get; private set; } = uniqueId;

    /// <summary>
    /// The attribute field to use on the user object.
    /// </summary>
    /// <example>
    /// sAMAccountName
    /// </example>
    public string UserName { get; private set; } = userName;

    /// <summary>
    /// The attribute field to use when loading the user full name.
    /// </summary>
    /// <example>
    /// displayName
    /// </example>
    public string DisplayName { get; private set; } = displayName;

    public LDAP Ldap { get; set; }
    public IdColumn LdapId { get; set; }

    public void Update(string uniqueId, string userName, string displayName)
    {
        UniqueId = Guard.Against.NullOrEmpty(uniqueId, nameof(uniqueId));
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName));
        DisplayName = Guard.Against.NullOrEmpty(displayName, nameof(displayName));
    }
}