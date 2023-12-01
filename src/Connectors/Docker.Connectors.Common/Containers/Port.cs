namespace Connectors.Docker.Containers;

public record Port(string Ip, int PrivatePort, int PublicPort, string Type);