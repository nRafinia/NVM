using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Command.DeleteFile;
using FileManager.Application.FileLists.Query.GetFileContent;
using Shared.Domain.Shared;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class DeleteFileCommandHandler : IHttpRequestHandler<DeleteFileCommand>
{
    private readonly IFileLogic _fileListLogic;

    public DeleteFileCommandHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var response = _fileListLogic.DeleteFile(request);
        return Task.FromResult(response.GetHttpResponse());
    }
}