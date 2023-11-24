using Agent.UI.Application.Abstractions.Models.FileManager;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.Abstractions.Interfaces;

public interface IFileManager
{
    Task<Result<GetPathResponse?>> GetPath(GetPathRequest request);
    Task<Result> CreateFolder(CreateFolderRequest request);
    Task<Result> DeleteFolder(DeleteFolderRequest request);
}