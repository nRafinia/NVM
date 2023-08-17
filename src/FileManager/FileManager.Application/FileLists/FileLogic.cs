using FileManager.Application.Abstraction;
using FileManager.Application.FileLists.Command.CreateFolder;
using FileManager.Application.FileLists.Command.DeleteFile;
using FileManager.Application.FileLists.Command.DeleteFolder;
using FileManager.Application.FileLists.Command.WriteToBinaryFile;
using FileManager.Application.FileLists.Command.WriteToTextFile;
using FileManager.Application.FileLists.Query.GetFileContent;
using FileManager.Application.FileLists.Query.GetFileList;
using FileManager.Application.FileLists.Query.GetTextFileContent;
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

    public Result<GetFileListResponse?> GetList(GetFileListQuery request)
    {
        if (request.Path is "." or "..")
        {
            return Result.Failure<GetFileListResponse>(SharedErrors.InvalidArguments);
        }

        var path = Path.Combine(request.Root, request.Path ?? string.Empty)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var getFilesResponse = _files.GetFiles(path);

        return getFilesResponse.IsFailure
            ? Result.Failure<GetFileListResponse>(getFilesResponse.Error!)
            : new GetFileListResponse(getFilesResponse.Value!, path);
    }

    #region File

    public async Task<Result<GetFileContentResponse?>> GetContent(GetFileContentQuery request,
        CancellationToken cancellationToken)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var getFileContent = await _files.GetFileContentAsync(path, cancellationToken);
        return getFileContent.IsFailure
            ? Result.Failure<GetFileContentResponse>(getFileContent.Error!)
            : new GetFileContentResponse(getFileContent.Value!, path);
    }

    public async Task<Result<GetTextFileContentResponse?>> GetTextContent(GetTextFileContentQuery request,
        CancellationToken cancellationToken)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var getFileContent = await _files.GetTextFileContentAsync(path, cancellationToken);
        return getFileContent.IsFailure
            ? Result.Failure<GetTextFileContentResponse>(getFileContent.Error!)
            : new GetTextFileContentResponse(getFileContent.Value!, path);
    }

    public async Task<Result> WriteToTextFile(WriteToTextFileCommand request, CancellationToken cancellationToken)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var writeToFileResponse = await _files.WriteToFile(path, request.Content, cancellationToken);
        return writeToFileResponse;
    }

    public async Task<Result> WriteToBinaryFile(WriteToBinaryFileCommand request, CancellationToken cancellationToken)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var writeToFileResponse = await _files.WriteToFile(path, request.Content, cancellationToken);
        return writeToFileResponse;
    }

    public Result DeleteFile(DeleteFileCommand request)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var deleteFileResponse = _files.DeleteFile(path);
        return deleteFileResponse;
    }

    #endregion

    #region Directory

    public Result CreateFolder(CreateFolderCommand request)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var createFolderResponse = _files.CreateFolder(path);
        return createFolderResponse;
    }

    public Result DeleteFolder(DeleteFolderCommand request)
    {
        var path = Path.Combine(request.Root, request.Path)
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var deleteFolderResponse = _files.DeleteFolder(path);
        return deleteFolderResponse;
    }

    #endregion
}