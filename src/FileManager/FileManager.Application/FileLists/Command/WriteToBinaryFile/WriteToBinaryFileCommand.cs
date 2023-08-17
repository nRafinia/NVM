using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.Application.FileLists.Command.WriteToBinaryFile;

public sealed record WriteToBinaryFileCommand(string Root, string Path, byte[] Content) : IHttpRequest;