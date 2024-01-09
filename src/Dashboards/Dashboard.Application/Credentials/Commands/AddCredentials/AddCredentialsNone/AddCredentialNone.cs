using SharedKernel.Base.Commands;
using SharedKernel.Entities;

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;

public record AddCredentialNone(string Name, string? Description = default) : ICommand<Credential>;