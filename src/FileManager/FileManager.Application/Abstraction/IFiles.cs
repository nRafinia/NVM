using FileManager.Domain.Models;
using Shared.Domain.Base.Results;

namespace FileManager.Application.Abstraction;

public interface IFiles
{
    Result<IList<FileListItem>?> GetFiles(string path);
    Task<Result<byte[]?>> GetFileContentAsync(string path, CancellationToken cancellationToken);
    Task<Result<string?>> GetTextFileContentAsync(string path, CancellationToken cancellationToken);
    Result CreateFolder(string path);
    Result DeleteFolder(string path);
    Task<Result> WriteToFile(string path, string content, CancellationToken cancellationToken);
    Task<Result> WriteToFile(string path, byte[] content, CancellationToken cancellationToken);
    Result DeleteFile(string path);
}