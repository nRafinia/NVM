using Dashboard.Domain.Entities.LDAPs;
using Dashboard.Domain.Entities.Users.Enums;
using Dashboard.Domain.Extensions;
using Dashboard.Domain.Helpers;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Domain.Entities.Users;

public class User : AuditableEntity
{
    public string UserName { get; }

    public string? Password { get; private set; }

    public string DisplayName { get; private set; }

    public UserStatus Status { get; private set; }

    public AuthorizerType AuthorizerType { get; }

    public LDAP? Ldap { get; private set; }

    #region Constructors

    private User(IdColumn id): base(id)
    {
        
    }
    
    private User(string userName, string? password, string displayName, AuthorizerType authorizerType,
        LDAP? ldap) : base(IdColumn.New)
    {
        UserName = Guard.Against.NullOrEmpty(userName, nameof(userName)).ToLower();
        Password = password is null
            ? password
            : SecretHasher.Hash(Guard.Against.Password(password, nameof(password)));
        DisplayName = Guard.Against.NullOrEmpty(displayName, nameof(displayName));
        Status = UserStatus.Active;
        AuthorizerType = authorizerType;

        Ldap = ldap;
    }

    private User(string userName, string? password, string displayName, AuthorizerType authorizerType)
        : this(userName, password, displayName, authorizerType, null)
    {
    }

    public static User Local(string userName, string password, string displayName)
    {
        return new(userName, password, displayName, AuthorizerType.Local);
    }

    public static User LDAP(string userName, string displayName, LDAP ldap)
    {
        return new(userName, null, displayName, AuthorizerType.LDAP, ldap);
    }

    #endregion

    #region Public methods

    public bool CheckPassword(string password)
    {
        return SecretHasher.Verify(password, Password);
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        Guard.Against.NullOrEmpty(oldPassword);

        if (!CheckPassword(oldPassword))
        {
            throw new ArgumentException("Old password is incorrect.");
        }

        Guard.Against.Password(newPassword, nameof(newPassword));
        Password = SecretHasher.Hash(newPassword);
    }

    public void SetPassword(string password)
    {
        Guard.Against.Password(password, nameof(password));
        Password = SecretHasher.Hash(password);
    }

    public void ChangeDisplayName(string displayName)
    {
        DisplayName = Guard.Against.NullOrEmpty(displayName, nameof(displayName));
    }

    public void Disable()
    {
        Status = UserStatus.Inactive;
    }

    public void Enable()
    {
        Status = UserStatus.Active;
    }

    #endregion
}