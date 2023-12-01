using Connectors.Docker.Containers;

namespace Docker.Connectors.API.Models;

internal class ContainerRaw
{
    public string Id { get; set; }
    public long Created { get; set; }
    public string Image { get; set; }
    public Dictionary<string, string> Labels { get; set; }
    public List<string> Names { get; set; }
    public List<Port> Ports { get; set; }
    public NetworkConfig NetworkSettings { get; set; }
    public string State { get; set; }
    public string Status { get; set; }
}