using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Query.GetFileContent;
using Shared.Domain.Shared;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class GetFileContentQueryHandler : IHttpRequestHandler<GetFileContentQuery>
{
    private readonly IFileLogic _fileListLogic;

    public GetFileContentQueryHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(GetFileContentQuery request, CancellationToken cancellationToken)
    {
        var getContentResponse = await _fileListLogic.GetContent(request, cancellationToken);

        if (getContentResponse.IsFailure)
        {
            return getContentResponse.GetHttpResponse();
        }

        var contentResponse = getContentResponse.Value!;
        var fileName = Path.GetFileName(contentResponse.Path);

        return Results.Bytes(getContentResponse.Value!.Content, Common.GetMimeType(fileName), fileName);
    }
}