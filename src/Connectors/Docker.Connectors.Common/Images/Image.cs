namespace Connectors.Docker.Images;

public class Image
{
    public string Id { get; }
    public string Repository { get; }
    public string Tag { get; }
    public DateTime CreatedAt { get; }
    public long Size { get; }
    public long SharedSize { get; }
    public long VirtualSize { get; }

    public Image(string id, string repository, string tag, DateTime createdAt, long size, long virtualSize,
        long sharedSize)
    {
        Id = id;
        Repository = repository;
        Tag = tag;
        CreatedAt = createdAt;
        Size = size;
        VirtualSize = virtualSize;
        SharedSize = sharedSize;
    }
}