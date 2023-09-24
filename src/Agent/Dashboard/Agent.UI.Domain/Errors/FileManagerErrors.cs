using Shared.Domain.Base;

namespace Agent.UI.Domain.Errors;

public static class FileManagerErrors
{
    public static Error DeleteFolderError(string message) => new("Folder.Delete", message);
    public static Error CreateFolderError(string message) => new("Folder.Create", message);
    public static Error GetPathError(string message) => new("Path.Ge", message);
}