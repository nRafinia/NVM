using Dashboard.Domain.Base.Commands;

namespace Dashboard.Application.Credentials.AddCredentials.AddCredentialsNone;

public record AddCredentialNone(string Name, string? Description = default) : ICommand;