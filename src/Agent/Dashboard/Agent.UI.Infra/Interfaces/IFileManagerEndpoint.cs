using Agent.UI.Application.Abstractions.Models.FileManager;
using Refit;

namespace Agent.UI.Infra.Interfaces;

public interface IFileManagerEndpoint
{
    [Get("/FileManager/Path")]
    Task<GetPathResponse> GetPath([Query] GetPathRequest request);    
    
    
    [Post("/FileManager/Directory")]
    Task CreateFolder([Body] CreateFolderRequest request);    
    
    [Delete("/FileManager/Directory")]
    Task DeleteFolder([Query] DeleteFolderRequest request);
}