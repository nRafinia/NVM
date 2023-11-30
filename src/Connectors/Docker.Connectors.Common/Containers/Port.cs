namespace Connectors.Docker.Containers;

public record Port(int PrivatePort, int PublicPort, string Type);