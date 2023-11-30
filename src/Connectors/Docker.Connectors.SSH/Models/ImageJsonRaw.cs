using System.Text.Json.Serialization;

namespace Docker.Connectors.SSH.Models;

internal class ImageJsonRaw
{
    [JsonPropertyName("ID")]
    public string Id { get; set; }

    [JsonPropertyName("Repository")]
    public string Repository { get; set; }

    [JsonPropertyName("Tag")]
    public string Tag { get; set; }

    [JsonPropertyName("CreatedAt")]
    public string Created { get; set; }
    
    [JsonPropertyName("Size")]
    public string Size { get; set; }

    [JsonPropertyName("SharedSize")]
    public string SharedSize { get; set; }
 
    [JsonPropertyName("VirtualSize")]
    public string VirtualSize { get; set; }
}
