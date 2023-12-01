using System.Text.Json.Serialization;

namespace Docker.Connectors.SSH.Models;

internal class ContainerJsonRaw
{
    [JsonPropertyName("CreatedAt")]
    public string CreatedAt { get; set; } = null!;

    [JsonPropertyName("ID")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("Image")]
    public string Image { get; set; } = null!;

    [JsonPropertyName("Labels")]
    public string Labels { get; set; } = null!;

    [JsonPropertyName("Names")]
    public string Names { get; set; } = null!;

    [JsonPropertyName("Networks")]
    public string Networks { get; set; } = null!;

    [JsonPropertyName("Ports")]
    public string Ports { get; set; } = null!;

    [JsonPropertyName("Size")]
    public string Size { get; set; } = null!;

    [JsonPropertyName("State")]
    public string State { get; set; } = null!;

    [JsonPropertyName("Status")]
    public string Status { get; set; } = null!;
}
