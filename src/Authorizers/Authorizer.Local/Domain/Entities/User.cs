using Ardalis.GuardClauses;
using Authorizer.Local.Domain.Enums;
using Authorizer.Local.Domain.Extensions;
using Authorizer.Local.Domain.Helpers;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Domain.Entities;

public class User(string userName, string password, string displayName) : AuditableEntity(IdColumn.New)
{
    public string UserName { get; } = Guard.Against.NullOrEmpty(userName, nameof(userName));

    public string Password { get; private set; } =
        SecretHasher.Hash(Guard.Against.Password(password, nameof(password)));

    public string DisplayName { get; private set; } = Guard.Against.NullOrEmpty(displayName, nameof(displayName));

    public UserStatus Status { get; private set; }
    
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