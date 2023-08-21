using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Command.WriteToTextFile;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class WriteToTextFileCommandHandler : IHttpRequestHandler<WriteToTextFileCommand>
{
    private readonly IFileLogic _fileListLogic;

    public WriteToTextFileCommandHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(WriteToTextFileCommand request, CancellationToken cancellationToken)
    {
        var response = await _fileListLogic.WriteToTextFile(request, cancellationToken);
        return response.GetHttpResponse();
    }
}