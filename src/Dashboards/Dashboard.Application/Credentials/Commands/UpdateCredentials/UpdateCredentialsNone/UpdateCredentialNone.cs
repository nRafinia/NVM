using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;

public record UpdateCredentialNone(IdColumn Id, string? Name, string? Description = default) : ICommand;