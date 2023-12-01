namespace Connectors.Docker.Networks;

public class Network
{
    public string Id { get; }
    public string Name { get; }
    public string Driver { get; }
    public string Scope { get; }
    public bool Internal { get; }
    public IDictionary<string, string> Labels { get; }
    public DateTime CreatedAt { get; }
    public bool IPv6 { get; }

    public Network(string id, string name, string driver, string scope, bool @internal,
        IDictionary<string, string> labels, DateTime createdAt, bool ipv6)
    {
        Id = id;
        Name = name;
        Driver = driver;
        Scope = scope;
        Internal = @internal;
        Labels = labels;
        CreatedAt = createdAt;
        IPv6 = ipv6;
    }
}