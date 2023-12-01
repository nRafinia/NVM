using System.Text.Json.Serialization;

namespace Docker.Connectors.SSH.Models;

public class NetworkJsonRaw
{
    [JsonPropertyName("CreatedAt")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("Driver")]
    public string Driver { get; set; }

    [JsonPropertyName("ID")]
    public string Id { get; set; }

    [JsonPropertyName("Internal")]
    public string Internal { get; set; }

    [JsonPropertyName("IPv6")]
    public string IPv6 { get; set; }

    [JsonPropertyName("Labels")]
    public string Labels { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Scope")]
    public string Scope { get; set; }
}
