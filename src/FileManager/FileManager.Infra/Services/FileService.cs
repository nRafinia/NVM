using FileManager.Application.Abstraction;
using FileManager.Domain.Models;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

namespace FileManager.Infra.Services;

public class FileService : IFiles
{
    public Result<IList<FileList>?> GetFiles(string path)
    {
        try
        {
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            var filesInformation = GetFileInformation(files);

            var result = new List<FileList>();
            result.AddRange(directories.Select(d => new FileList(d, FileType.Directory, 0)));
            result.AddRange(filesInformation.Select(f => new FileList(f.Key, FileType.File, f.Value)));

            return result;
        }
        catch
        {
            return Result.Failure<IList<FileList>>(SharedErrors.DiskError);
        }
    }

    private Dictionary<string, long> GetFileInformation(IEnumerable<string> files)
    {
        var result = files.ToDictionary(f => f, GetFileInformation);
        return result;
    }

    private static long GetFileInformation(string path)
    {
        try
        {
            var fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
        catch
        {
            return 0;
        }
    }
}