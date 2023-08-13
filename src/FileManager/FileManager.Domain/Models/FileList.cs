namespace FileManager.Domain.Models;

public class FileListItem
{
    public string Name { get; }
    public FileType Type { get; }
    public long Size { get; }

    public FileListItem(string name, FileType type, long size)
    {
        Name = name;
        Type = type;
        Size = size;
    }
}