using FileManager.Application.FileLists.Models;
using FileManager.Domain.Models;
using Shared.Domain.Base.Results;

namespace FileManager.Application.FileLists;

public interface IFileListLogic
{
    Result<IList<FileListItem>?> GetFileList(GetFileListRequest request);
}