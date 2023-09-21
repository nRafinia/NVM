namespace Agent.UI.Application.Abstractions.Models.FileManager;

public sealed record DeleteFolderRequest(string Root, string Path);