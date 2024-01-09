using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

public record UpdateCredentialBasic(
    IdColumn Id,
    string? Name,
    string? UserName,
    string? Password,
    string? Description = default) : ICommand<Credential>;