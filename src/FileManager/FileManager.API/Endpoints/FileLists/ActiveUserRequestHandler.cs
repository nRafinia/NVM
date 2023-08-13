using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Models;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.FileLists;

public class ActiveUserRequestHandler : IHttpRequestHandler<GetFileListRequest>
{
    private readonly IFileListLogic _fileListLogic;

    public ActiveUserRequestHandler(IFileListLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(GetFileListRequest request, CancellationToken cancellationToken)
    {
        var result = _fileListLogic.GetFileList(request);
        return Task.FromResult(result.GetHttpResponse());
    }
}
