using FileManager.Application.Abstraction;
using FileManager.Domain.Errors;
using FileManager.Domain.Models;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;
using Shared.Domain.Shared;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem
// ReSharper disable ConvertClosureToMethodGroup

namespace FileManager.Infra.Services;

public class FileService : IFiles
{
    private readonly ILogger<FileService> _logger;

    public FileService(ILogger<FileService> logger)
    {
        _logger = logger;
    }

    public Result<IList<FileListItem>?> GetFiles(string path)
    {
        try
        {
            if (!Path.Exists(path))
            {
                return Result.Failure<IList<FileListItem>>(FileErrors.DirectoryNotExists);
            }

            var directories = Directory.GetDirectories(path)
                .Select(d => Path.GetFileName(d))
                .ToList();
            var files = Directory.GetFiles(path)
                .Select(f => Path.GetFileName(f))
                .ToList();

            var filesInformation = GetFileInformation(path, files);
            var directoriesInformation = GetDirectoryInformation(path, directories);

            var result = new List<FileListItem>();
            result.AddRange(directoriesInformation
                .Select(d => new FileListItem(d.Key, FileType.Directory, "Directory", 0, d.Value?.LastWriteTimeUtc)));

            result.AddRange(filesInformation
                .Select(f =>
                    new FileListItem(f.Key, FileType.File, Common.GetMimeType(f.Key), f.Value?.Length ?? 0,
                        f.Value?.LastWriteTimeUtc)));

            return result;
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<FileListItem>>(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<IList<FileListItem>>(SharedErrors.DiskError);
        }
    }

    #region File

    public async Task<Result<byte[]?>> GetFileContentAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(path))
            {
                return Result.Failure<byte[]>(SharedErrors.InvalidArguments);
            }

            return await File.ReadAllBytesAsync(path, cancellationToken);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<byte[]>(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<byte[]>(SharedErrors.DiskError);
        }
    }

    public async Task<Result<string?>> GetTextFileContentAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(path))
            {
                return Result.Failure<string>(SharedErrors.InvalidArguments);
            }

            return await File.ReadAllTextAsync(path, cancellationToken);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<string>(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<string>(SharedErrors.DiskError);
        }
    }

    public async Task<Result> WriteToFile(string path, string content, CancellationToken cancellationToken)
    {
        try
        {
            if (path.Any(p => Path.GetInvalidPathChars().Contains(p)))
            {
                return Result.Failure(FileErrors.InvalidCharacter);
            }

            var directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                return Result.Failure(FileErrors.DirectoryNotExists);
            }

            await File.WriteAllTextAsync(path, content, cancellationToken);

            return Result.Success();
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.DiskError);
        }
    }

    public async Task<Result> WriteToFile(string path, byte[] content, CancellationToken cancellationToken)
    {
        try
        {
            if (path.Any(p => Path.GetInvalidPathChars().Contains(p)))
            {
                return Result.Failure(FileErrors.InvalidCharacter);
            }

            var directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                return Result.Failure(FileErrors.DirectoryNotExists);
            }

            await File.WriteAllBytesAsync(path, content, cancellationToken);

            return Result.Success();
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.DiskError);
        }
    }

    public Result DeleteFile(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                return Result.Failure(FileErrors.FileNotExists);
            }

            File.Delete(path);

            return Result.Success();
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.DiskError);
        }
    }
    
    #endregion

    #region Directory

    public Result CreateFolder(string path)
    {
        try
        {
            if (path.Any(p => Path.GetInvalidPathChars().Contains(p)))
            {
                return Result.Failure(FileErrors.InvalidCharacter);
            }

            if (Directory.Exists(path))
            {
                return Result.Failure(FileErrors.DirectoryExists);
            }

            Directory.CreateDirectory(path);

            return Result.Success();
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.DiskError);
        }
    }

    public Result DeleteFolder(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                return Result.Failure(FileErrors.DirectoryNotExists);
            }

            Directory.Delete(path, true);

            return Result.Success();
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.AccessDenied);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure(SharedErrors.DiskError);
        }
    }

    #endregion

    #region Private methods

    private static Dictionary<string, FileInfo?> GetFileInformation(string path, IEnumerable<string> files)
    {
        var result = files.ToDictionary(f => f, f => GetFileInformation(Path.Combine(path, f)));
        return result;
    }

    private static FileInfo? GetFileInformation(string path)
    {
        try
        {
            var fileInfo = new FileInfo(path);
            return fileInfo;
        }
        catch
        {
            return default;
        }
    }

    private static Dictionary<string, DirectoryInfo?> GetDirectoryInformation(string path, IEnumerable<string> files)
    {
        var result = files.ToDictionary(f => f, f => GetDirectoryInformation(Path.Combine(path, f)));
        return result;
    }

    private static DirectoryInfo? GetDirectoryInformation(string path)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo;
        }
        catch
        {
            return default;
        }
    }

    #endregion
}