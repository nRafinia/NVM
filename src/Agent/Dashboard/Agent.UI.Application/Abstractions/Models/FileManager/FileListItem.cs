namespace Agent.UI.Application.Abstractions.Models.FileManager;

public record FileListItem(string Name, FileType Type, string MimeType, long Size, DateTime? ModifyDate);