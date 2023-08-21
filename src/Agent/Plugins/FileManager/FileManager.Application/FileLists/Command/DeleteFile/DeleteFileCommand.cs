using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Command.DeleteFile;

public sealed record DeleteFileCommand(string Root, string Path) : IHttpRequest;