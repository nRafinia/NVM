namespace Creator.Shared.Models;

public record Image(string Id, string Repository, string Tag, string Digest, DateTime Created, string Size);