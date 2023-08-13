using FileManager.Application.Abstraction;
using FileManager.Domain.Models;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

// ReSharper disable ConvertClosureToMethodGroup

namespace FileManager.Infra.Services;

public class FileService : IFiles
{
    public Result<IList<FileListItem>?> GetFiles(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                return Result.Failure<IList<FileListItem>>(SharedErrors.InvalidArguments);
            }

            var directories = Directory.GetDirectories(path)
                .Select(d => Path.GetFileName(d));
            var files = Directory.GetFiles(path)
                .Select(f => Path.GetFileName(f))
                .ToList();

            var filesInformation = GetFileInformation(path, files);

            var result = new List<FileListItem>();
            result.AddRange(directories.Select(d => new FileListItem(d, FileType.Directory, 0)));
            result.AddRange(filesInformation.Select(f => new FileListItem(f.Key, FileType.File, f.Value)));

            return result;
        }
        catch
        {
            return Result.Failure<IList<FileListItem>>(SharedErrors.DiskError);
        }
    }

    private static Dictionary<string, long> GetFileInformation(string path, IEnumerable<string> files)
    {
        var result = files.ToDictionary(f => f, f => GetFileInformation(Path.Combine(path, f)));
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