using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Command.CreateFolder;

public sealed record CreateFolderCommand(string Root, string Path) : IHttpRequest;