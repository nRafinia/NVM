using FileManager.Domain.Models;
using Shared.Domain.Base.Results;

namespace FileManager.Application.Abstraction;

public interface IFiles
{
    Result<IList<FileListItem>?> GetFiles(string path);
    Task<Result<byte[]?>> GetFileContentAsync(string path, CancellationToken cancellationToken);
    Task<Result<string?>> GetTextFileContentAsync(string path, CancellationToken cancellationToken);
}