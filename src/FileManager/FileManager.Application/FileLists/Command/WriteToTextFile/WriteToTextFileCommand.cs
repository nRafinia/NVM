using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Command.WriteToTextFile;

public sealed record WriteToTextFileCommand(string Root, string Path, string Content) : IHttpRequest;