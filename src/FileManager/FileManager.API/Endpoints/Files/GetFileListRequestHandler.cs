using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Models.GetFileList;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class GetFileListRequestHandler : IHttpRequestHandler<GetFileListRequest>
{
    private readonly IFileLogic _fileListLogic;

    public GetFileListRequestHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(GetFileListRequest request, CancellationToken cancellationToken)
    {
        var result = _fileListLogic.GetList(request);
        return Task.FromResult(result.GetHttpResponse());
    }
}
