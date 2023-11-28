
namespace Connectors.Docker.Images;

public class Image
{
    public string Id { get; }
    public string Repository { get; }
    public string Tag { get; }
    public string Digest { get; }
    public DateTime CreatedAt { get; }
    public string CreatedSince { get; }
    public long Size { get;  }
    public long? VirtualSize { get; }
    public long? SharedSize { get;  }
    public long? UniqueSize { get;  }
    public string? Containers { get;  }

    public Image()
    {
        
    }
    
}
