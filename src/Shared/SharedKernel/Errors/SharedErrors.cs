using System.Xml;

namespace SharedKernel.Errors;

public static class SharedErrors
{
    public const string InvalidArgumentsCode = "Invalid.Arguments";
    public const string DuplicateNameCode = "Duplicate.Name";
    public const string ItemNotFoundCode = "NotFound";
    public const string InternalErrorCode = "InternalError";

    public static readonly Error InvalidArguments = new(InvalidArgumentsCode, "The provided information is not valid");

    public static readonly Error InvalidCredentialType =
        new("Invalid.CredentialType", "The provided credential type is not valid");

    public static readonly Error InvalidCredential = new("Invalid.Credential", "The provided credential is not valid");

    public static Error Duplicate(string message) => new(DuplicateNameCode, message);
    public static readonly Error AuthorizationIsEmpty = new("Authorization.IsEmpty", "Authorization token is empty");
    public static readonly Error AuthorizationIsFailed = new("Authorization.IsEmpty", "Invalid authorization token");
    public static readonly Error InternalError = new(InternalErrorCode, "An internal error has occurred");
    public static Error InternalErrorMessage(string message) => new(InternalErrorCode, message);
    public static Error ConnectToServer(string message) => new("Server.Error", message);
    public static readonly Error ProviderError = new("Provider.Error", "An error occurred in the service provider");
    public static readonly Error SaveToDiskError = new("Disk.SaveError", "Error in save file to disk.");
    public static readonly Error AccessDenied = new("Access.Denied", "Access denied.");
    public static readonly Error DiskError = new("Disk.Error", "Error in access to disk.");
    public static readonly Error NotAllowed = new("Authorize.NotAllowed", "You are not allowed to do this.");
    public static readonly Error OutOfRang = new("Invalid.OutOfRang", "userId is out of range.");
    public static readonly Error ItemNotFound = new(ItemNotFoundCode, "No items found with the provided information");

    public static readonly Error ProviderValidationError =
        new("Provider.Validation.Errors", "Could not validate the data on the service provider side");

    public static readonly Error UnknownError = new("Provider.Errors", "An unknown error has occurred");

    public static Error ValidationError(Dictionary<string, string[]> errors) =>
        new Error(InvalidArgumentsCode, "The provided information is not valid", errors);
}