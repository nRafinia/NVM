namespace Agent.UI.Application.Abstractions.Models.FileManager;

public sealed record GetPathResponse(IList<FileListItem> Items, string Path);