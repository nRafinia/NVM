namespace Creator.Shared.Models;

public record MakeVirtualRequest(string Name, string Image, string? Network, string? Port, string? Volume);