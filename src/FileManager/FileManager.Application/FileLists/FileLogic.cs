using FileManager.Application.Abstraction;
using FileManager.Application.FileLists.Models.GetFileContent;
using FileManager.Application.FileLists.Models.GetFileList;
using FileManager.Application.FileLists.Models.GetTextFileContent;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

namespace FileManager.Application.FileLists;

public class FileLogic : IFileLogic
{
    private readonly IFiles _files;

    public FileLogic(IFiles files)
    {
        _files = files;
    }

    public Result<GetFileListResponse?> GetList(GetFileListRequest request)
    {
        if (request.Path is "." or "..")
        {
            return Result.Failure<GetFileListResponse>(SharedErrors.InvalidArguments);
        }

        var path = string.Empty;
        if (!string.IsNullOrWhiteSpace(request.Path))
        {
            path = request.Path
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

        //var rootPath = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
        var getFilesResponse = _files.GetFiles(Path.Combine(request.Root, path));

        return getFilesResponse.IsFailure
            ? Result.Failure<GetFileListResponse>(getFilesResponse.Error!)
            : new GetFileListResponse(getFilesResponse.Value!, path);
    }

    public async Task<Result<GetFileContentResponse?>> GetContent(GetFileContentRequest request,
        CancellationToken cancellationToken)
    {
        var path = string.Empty;
        if (!string.IsNullOrWhiteSpace(request.Path))
        {
            path = request.Path
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

        var getFileContent = await _files.GetFileContentAsync(Path.Combine(request.Root, path), cancellationToken);
        return getFileContent.IsFailure
            ? Result.Failure<GetFileContentResponse>(getFileContent.Error!)
            : new GetFileContentResponse(getFileContent.Value!, path);
    }

    public async Task<Result<GetTextFileContentResponse?>> GetTextContent(GetTextFileContentRequest request,
        CancellationToken cancellationToken)
    {
        var path = string.Empty;
        if (!string.IsNullOrWhiteSpace(request.Path))
        {
            path = request.Path
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

        var getFileContent = await _files.GetTextFileContentAsync(Path.Combine(request.Root, path), cancellationToken);
        return getFileContent.IsFailure
            ? Result.Failure<GetTextFileContentResponse>(getFileContent.Error!)
            : new GetTextFileContentResponse(getFileContent.Value!, path);
    }
}