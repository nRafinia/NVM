using FileManager.Application.Abstraction;
using FileManager.Application.FileLists.Models;
using FileManager.Domain.Models;
using Shared.Domain.Base.Results;

namespace FileManager.Application.FileLists;

public class FileListLogic : IFileListLogic
{
    private readonly IFiles _files;

    private const string RootPath = @"d:\";

    public FileListLogic(IFiles files)
    {
        _files = files;
    }

    public Result<IList<FileListItem>?> GetFileList(GetFileListRequest request)
    {
        var path = string.IsNullOrWhiteSpace(request.Path)
            ? RootPath
            : Path.Combine(RootPath + request.Path);
        var getFilesResponse = _files.GetFiles(path);

        return getFilesResponse;
    }
}