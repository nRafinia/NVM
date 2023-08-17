using FileManager.Domain.Models;

namespace FileManager.Application.FileLists.Query.GetFileList;

public sealed record GetFileListResponse(IList<FileListItem> Items, string Path);