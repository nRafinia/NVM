using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Models;

public record GetFileListRequest(string? Path = null) : IHttpRequest;