using SharedKernel.Base;

namespace Dashboard.Domain.Entities.LDAPs;

public class LdapAttribute(
    string uniqueId = "objectGUID",
    string userName = "sAMAccountName",
    string displayName = "displayName") : ValueObject
{
    /// <summary>
    /// The attribute field to use for tracking user identity across user renames
    /// </summary>
    /// <example>
    /// objectGUID
    /// </example>
    public string UniqueId { get; private set; } = Guard.Against.NullOrEmpty(uniqueId, nameof(uniqueId));

    /// <summary>
    /// The attribute field to use on the user object.
    /// </summary>
    /// <example>
    /// sAMAccountName
    /// </example>
    public string UserName { get; private set; } = Guard.Against.NullOrEmpty(userName, nameof(userName));

    /// <summary>
    /// The attribute field to use when loading the user full name.
    /// </summary>
    /// <example>
    /// displayName
    /// </example>
    public string DisplayName { get; private set; } = Guard.Against.NullOrEmpty(displayName, nameof(displayName));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return UniqueId;
        yield return UserName;
        yield return DisplayName;
    }
}