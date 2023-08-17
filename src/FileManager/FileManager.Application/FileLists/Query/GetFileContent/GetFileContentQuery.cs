using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Query.GetFileContent;

public sealed record GetFileContentQuery(string Root, string Path):IHttpRequest;