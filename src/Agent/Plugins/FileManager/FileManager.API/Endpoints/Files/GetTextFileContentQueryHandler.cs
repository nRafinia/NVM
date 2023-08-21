using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Query.GetTextFileContent;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class GetTextFileContentQueryHandler : IHttpRequestHandler<GetTextFileContentQuery>
{
    private readonly IFileLogic _fileListLogic;

    public GetTextFileContentQueryHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(GetTextFileContentQuery request, CancellationToken cancellationToken)
    {
        var getContentResponse = await _fileListLogic.GetTextContent(request, cancellationToken);
        return getContentResponse.IsFailure
            ? getContentResponse.GetHttpResponse()
            : Results.Content(getContentResponse.Value!.Content);
    }
}