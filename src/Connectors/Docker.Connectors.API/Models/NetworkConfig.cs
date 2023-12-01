namespace Docker.Connectors.API.Models;

internal class NetworkConfig
{
    public Dictionary<string, Host> Networks { get; set; }
}