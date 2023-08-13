using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Models.GetFileContent;
using FileManager.Application.FileLists.Models.GetFileList;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class GetFileContentRequestHandler : IHttpRequestHandler<GetFileContentRequest>
{
    private readonly IFileLogic _fileListLogic;

    public GetFileContentRequestHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(GetFileContentRequest request, CancellationToken cancellationToken)
    {
        var getContentResponse = await _fileListLogic.GetContent(request, cancellationToken);

        if (getContentResponse.IsFailure)
        {
            return getContentResponse.GetHttpResponse();
        }

        var contentResponse = getContentResponse.Value!;
        var fileName = Path.GetFileName(contentResponse.Path);

        return Results.Bytes(getContentResponse.Value!.Content, fileDownloadName: fileName);
    }
}