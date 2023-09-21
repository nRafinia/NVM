using Agent.UI.Application.Abstractions.Models.FileManager;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.FileManager;

public interface IFileManagerLogic
{
    Task<Result<GetPathResponse?>> GetPath(GetPathRequest request);

    Task<Result> CreateFolder(CreateFolderRequest request);
    Task<Result> DeleteFolder(DeleteFolderRequest request);
}