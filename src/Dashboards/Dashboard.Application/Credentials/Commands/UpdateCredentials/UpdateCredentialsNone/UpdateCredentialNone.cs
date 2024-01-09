using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;

public record UpdateCredentialNone(IdColumn Id, string? Name, string? Description = default) : ICommand;