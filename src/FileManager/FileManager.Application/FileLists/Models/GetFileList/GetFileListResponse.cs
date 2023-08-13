using FileManager.Domain.Models;

namespace FileManager.Application.FileLists.Models.GetFileList;

public record GetFileListResponse(IList<FileListItem> Items, string Path);