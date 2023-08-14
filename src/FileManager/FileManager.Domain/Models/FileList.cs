namespace FileManager.Domain.Models;

public record FileListItem(string Name, FileType Type, string MimeType, long Size);