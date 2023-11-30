using System.Text.Json.Serialization;

namespace Docker.Connectors.API.Models;

public class ImageRaw
{
    [JsonPropertyName("Id")]
    public string Id { get; set; }
    
    [JsonPropertyName("RepoTags")]
    public List<string> RepoTags { get; set; }
    
    [JsonPropertyName("Created")]
    public int Created { get; set; }

    [JsonPropertyName("Size")]
    public int Size { get; set; }

    [JsonPropertyName("SharedSize")]
    public int SharedSize { get; set; }

    [JsonPropertyName("VirtualSize")]
    public int VirtualSize { get; set; }
}
