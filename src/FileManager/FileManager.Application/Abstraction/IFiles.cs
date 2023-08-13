using FileManager.Domain.Models;
using Shared.Domain.Base.Results;

namespace FileManager.Application.Abstraction;

public interface IFiles
{
    Result<IList<FileList>?> GetFiles(string path);
}