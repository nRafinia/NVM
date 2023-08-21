using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Query.GetTextFileContent;

public sealed record GetTextFileContentQuery(string Root, string Path):IHttpRequest;