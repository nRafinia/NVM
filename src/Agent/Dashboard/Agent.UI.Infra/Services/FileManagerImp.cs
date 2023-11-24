using Agent.UI.Application.Abstractions.Interfaces;
using Agent.UI.Application.Abstractions.Models.FileManager;
using Agent.UI.Domain.Errors;
using Agent.UI.Infra.Interfaces;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Agent.UI.Infra.Services;

public class FileManagerImp : IFileManager
{
    private readonly IFileManagerEndpoint _service;
    private readonly ILogger<FileManagerImp> _logger;

    public FileManagerImp(IFileManagerEndpoint service, ILogger<FileManagerImp> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result<GetPathResponse?>> GetPath(GetPathRequest request)
    {
        try
        {
            return await _service.GetPath(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetPathResponse>(FileManagerErrors.GetPathError(e.Message));
        }
    }    
    
    public async Task<Result> CreateFolder(CreateFolderRequest request)
    {
        try
        {
            await _service.CreateFolder(request);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(FileManagerErrors.CreateFolderError(e.Message));
        }
    }    
    
    public async Task<Result> DeleteFolder(DeleteFolderRequest request)
    {
        try
        {
            await _service.DeleteFolder(request);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(FileManagerErrors.DeleteFolderError(e.Message));
        }
    }
}