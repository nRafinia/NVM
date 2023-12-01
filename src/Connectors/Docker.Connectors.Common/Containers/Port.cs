namespace Connectors.Docker.Containers;

public record Port(string IP, int PrivatePort, int PublicPort, string Type);