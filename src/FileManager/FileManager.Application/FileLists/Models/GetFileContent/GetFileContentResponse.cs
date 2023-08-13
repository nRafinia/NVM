namespace FileManager.Application.FileLists.Models.GetFileContent;

public record GetFileContentResponse(byte[] Content, string Path);