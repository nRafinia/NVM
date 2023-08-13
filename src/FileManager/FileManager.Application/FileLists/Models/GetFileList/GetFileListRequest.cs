using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Models.GetFileList;

public record GetFileListRequest(string Root, string? Path = null) : IHttpRequest;