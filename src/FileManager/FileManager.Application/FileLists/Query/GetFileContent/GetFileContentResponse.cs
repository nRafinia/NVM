namespace FileManager.Application.FileLists.Query.GetFileContent;

public sealed record GetFileContentResponse(byte[] Content, string Path);