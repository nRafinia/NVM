using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Command.CreateFolder;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Directories;

public class CreateFolderCommandHandler : IHttpRequestHandler<CreateFolderCommand>
{
    private readonly IFileLogic _fileListLogic;

    public CreateFolderCommandHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var response = _fileListLogic.CreateFolder(request);
        return Task.FromResult(response.GetHttpResponse());
    }
}