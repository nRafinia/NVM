using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Command.DeleteFolder;

public sealed record DeleteFolderCommand(string Root, string Path) : IHttpRequest;