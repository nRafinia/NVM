using Shared.Domain.Base;

namespace Shared.Domain.Errors;

public static class AuthenticateErrors
{
    public const string TokenIsExpiredName = "Auth.ExpiredToken";
    public const string AuthenticationFailedName = "Auth.AuthenticationFiled";
    public const string AccountIsNotActiveName = "Auth.AccountIsNotActive";
    
    public static Error ServiceError(string statusCode, string message) => new($"Service.Errors.{statusCode}", message);
    public static readonly Error NoCredentials = new("Authenticate.NoCredentials", "No credentials.");
    public static Error InvalidCredential(string header) => new("Authenticate.InvalidCredential", $"Invalid credential, {header}");
    public static Error InvalidCredentialKey(string header) => new("Authenticate.InvalidCredentialKey", $"Invalid credential key, {header}");
    
    public static readonly Error AuthenticationFailed =
        new Error(AuthenticationFailedName, "User name or password is incorrect");

    public static readonly Error TokenIsExpired = new(TokenIsExpiredName, "Token is expired");
    public static readonly Error AccountIsNotActive = new(AccountIsNotActiveName, "Your account is not active");
}