namespace Connectors.Docker.Containers;

public class Container
{
    public string Id { get; }
    public DateTime CreatedAt { get; }
    public string Image { get; }
    public IDictionary<string, string> Labels { get; }
    public IList<string> Names { get; }
    public List<string> Networks { get; }
    public List<Port> Ports { get; }
    public string State { get; }
    public string Status { get; }

    public Container(string id, DateTime createdAt, string image, IDictionary<string, string> labels, IList<string> names,
        List<string> networks, List<Port> ports, string state, string status)
    {
        Id = id;
        CreatedAt = createdAt;
        Image = image;
        Labels = labels;
        Names = names;
        Networks = networks;
        Ports = ports;
        State = state;
        Status = status;
    }
}