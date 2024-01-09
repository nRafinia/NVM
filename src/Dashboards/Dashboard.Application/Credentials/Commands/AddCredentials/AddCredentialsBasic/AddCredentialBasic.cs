using SharedKernel.Base.Commands;

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsBasic;

public record AddCredentialBasic(string Name, string UserName, string Password, string? Description = default)
    : ICommand;