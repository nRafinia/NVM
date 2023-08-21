using Shared.Domain.Base;

namespace FileManager.Domain.Errors;

public static class FileErrors
{
    public static Error DirectoryExists = new("Directory.Exists", "The desired directory already exists");
    public static Error DirectoryNotExists = new("Directory.NotExists", "The desired directory not exists");
    public static Error InvalidCharacter = new("Invalid.Character", "Invalid character in name");
    public static Error FileNotExists = new("File.NotExists", "The desired file not exists");

}