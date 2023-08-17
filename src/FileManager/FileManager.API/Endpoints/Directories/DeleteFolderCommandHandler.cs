using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Command.CreateFolder;
using FileManager.Application.FileLists.Command.DeleteFolder;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Directories;

public class DeleteFolderCommandHandler : IHttpRequestHandler<DeleteFolderCommand>
{
    private readonly IFileLogic _fileListLogic;

    public DeleteFolderCommandHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        var response = _fileListLogic.DeleteFolder(request);
        return Task.FromResult(response.GetHttpResponse());
    }
}