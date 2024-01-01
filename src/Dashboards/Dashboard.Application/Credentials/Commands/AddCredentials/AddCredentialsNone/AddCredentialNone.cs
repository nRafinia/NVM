namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;

public record AddCredentialNone(string Name, string? Description = default) : ICommand;