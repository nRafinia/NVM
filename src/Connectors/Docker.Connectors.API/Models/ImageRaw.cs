using System.Text.Json.Serialization;

namespace Docker.Connectors.API.Models;

internal class ImageRaw
{
    [JsonPropertyName("Id")]
    public string Id { get; set; }
    
    [JsonPropertyName("RepoTags")]
    public List<string> RepoTags { get; set; }
    
    [JsonPropertyName("Created")]
    public long Created { get; set; }

    [JsonPropertyName("Size")]
    public long Size { get; set; }

    [JsonPropertyName("SharedSize")]
    public long SharedSize { get; set; }

    [JsonPropertyName("VirtualSize")]
    public long VirtualSize { get; set; }
}
