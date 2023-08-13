using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Models.GetTextFileContent;

public record GetTextFileContentRequest(string Root, string Path):IHttpRequest;