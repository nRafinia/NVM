using Dashboard.Domain.Base.Commands;

namespace Dashboard.Application.Credentials.AddCredentials.AddCredentialsBasic;

public record AddCredentialBasic(string Name, string UserName, string Password, string? Description = default)
    : ICommand;