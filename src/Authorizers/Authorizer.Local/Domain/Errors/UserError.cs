using SharedKernel.Base;

namespace Authorizer.Local.Domain.Errors;

public static class UserError
{
    public static readonly Error UserIsInactive = new("user.inactive", "User is inactive.");
    public static readonly Error PasswordIsInvalid = new("user.password.invalid", "Password is invalid.");
}