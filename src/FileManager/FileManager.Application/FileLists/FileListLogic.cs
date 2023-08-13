using FileManager.Application.Abstraction;
using FileManager.Application.FileLists.Models;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

namespace FileManager.Application.FileLists;

public class FileListLogic : IFileListLogic
{
    private readonly IFiles _files;

    public FileListLogic(IFiles files)
    {
        _files = files;
    }

    public Result<GetFileListResponse?> GetFileList(GetFileListRequest request)
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
}