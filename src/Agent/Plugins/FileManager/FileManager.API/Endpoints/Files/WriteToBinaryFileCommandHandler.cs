using FileManager.Application.FileLists;
using FileManager.Application.FileLists.Command.WriteToBinaryFile;
using Microsoft.AspNetCore.Http;
using Shared.Presentation.Extensions;
using Shared.Presentation.HttpEndpointHandlers;

namespace FileManager.API.Endpoints.Files;

public class WriteToBinaryFileCommandHandler : IHttpRequestHandler<WriteToBinaryFileCommand>
{
    private readonly IFileLogic _fileListLogic;

    public WriteToBinaryFileCommandHandler(IFileLogic fileListLogic)
    {
        _fileListLogic = fileListLogic;
    }

    public async Task<IResult> Handle(WriteToBinaryFileCommand request, CancellationToken cancellationToken)
    {
        var response =await _fileListLogic.WriteToBinaryFile(request,cancellationToken);
        return response.GetHttpResponse();
    }
}