using FileManager.Application.FileLists.Models;
using FileManager.Application.FileLists.Models.GetFileContent;
using FileManager.Application.FileLists.Models.GetFileList;
using FileManager.Application.FileLists.Models.GetTextFileContent;
using Shared.Domain.Base.Results;

namespace FileManager.Application.FileLists;

public interface IFileLogic
{
    Result<GetFileListResponse?> GetList(GetFileListRequest request);

    Task<Result<GetFileContentResponse?>> GetContent(GetFileContentRequest request,
        CancellationToken cancellationToken);

    Task<Result<GetTextFileContentResponse?>> GetTextContent(GetTextFileContentRequest request,
        CancellationToken cancellationToken);
}