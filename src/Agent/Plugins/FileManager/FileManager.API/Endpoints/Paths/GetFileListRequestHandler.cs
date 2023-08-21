using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Query.GetFileList;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Paths;

public class GetFileListRequestHandler : IHttpRequestHandler<GetFileListQuery>
{
    private readonly IFileLogic _fileListLogic;

    public GetFileListRequestHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public Task<IResult> Handle(GetFileListQuery request, CancellationToken cancellationToken)
    {
        var response = _fileListLogic.GetList(request);
        return Task.FromResult(response.GetHttpResponse());
    }
}
