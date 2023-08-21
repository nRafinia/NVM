using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Query.GetFileList;

public sealed record GetFileListQuery(string Root, string? Path = null) : IHttpRequest;