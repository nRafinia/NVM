using FileManager.Application.FileLists.Command.CreateFolder;
using FileManager.Application.FileLists.Command.DeleteFile;
using FileManager.Application.FileLists.Command.DeleteFolder;
using FileManager.Application.FileLists.Command.WriteToBinaryFile;
using FileManager.Application.FileLists.Command.WriteToTextFile;
using FileManager.Application.FileLists.Query.GetFileContent;
using FileManager.Application.FileLists.Query.GetFileList;
using FileManager.Application.FileLists.Query.GetTextFileContent;
using Shared.Domain.Base.Results;

namespace FileManager.Application.FileLists;

public interface IFileLogic
{
    Result<GetFileListResponse?> GetList(GetFileListQuery request);

    Task<Result<GetFileContentResponse?>> GetContent(GetFileContentQuery request,
        CancellationToken cancellationToken);

    Task<Result<GetTextFileContentResponse?>> GetTextContent(GetTextFileContentQuery request,
        CancellationToken cancellationToken);

    
    Result DeleteFolder(DeleteFolderCommand request);
    Result CreateFolder(CreateFolderCommand request);
    Task<Result> WriteToTextFile(WriteToTextFileCommand request, CancellationToken cancellationToken);
    Task<Result> WriteToBinaryFile(WriteToBinaryFileCommand request, CancellationToken cancellationToken);
    Result DeleteFile(DeleteFileCommand request);
}