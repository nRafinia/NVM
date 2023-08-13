using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Models.GetTextFileContent;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class GetTextFileContentRequestHandler : IHttpRequestHandler<GetTextFileContentRequest>
{
    private readonly IFileLogic _fileListLogic;

    public GetTextFileContentRequestHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(GetTextFileContentRequest request, CancellationToken cancellationToken)
    {
        var getContentResponse = await _fileListLogic.GetTextContent(request, cancellationToken);
        return getContentResponse.IsFailure
            ? getContentResponse.GetHttpResponse()
            : Results.Content(getContentResponse.Value!.Content);
    }
}