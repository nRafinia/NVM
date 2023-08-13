using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Models.GetFileContent;

public record GetFileContentRequest(string Root, string Path):IHttpRequest;