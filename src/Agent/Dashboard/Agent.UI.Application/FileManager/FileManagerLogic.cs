using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.FileManager;
using Shared.Domain.Base.Results;

namespace Agent.UI.Application.FileManager;

public class FileManagerLogic : IFileManagerLogic
{
    private readonly IFileManager _fileManager;

    public FileManagerLogic(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public Task<Result<GetPathResponse?>> GetPath(GetPathRequest request)
        => _fileManager.GetPath(request);    
}