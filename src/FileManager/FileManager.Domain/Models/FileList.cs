namespace FileManager.Domain.Models;

public class FileList
{
    public string Name { get; }
    public FileType Type { get; }
    public long Size { get; }

    public FileList(string name, FileType type, long size)
    {
        Name = name;
        Type = type;
        Size = size;
    }
}