using FileManager.Domain.Models;

namespace FileManager.Application.FileLists.Models;

public record GetFileListResponse(IList<FileListItem> Items, string Path);