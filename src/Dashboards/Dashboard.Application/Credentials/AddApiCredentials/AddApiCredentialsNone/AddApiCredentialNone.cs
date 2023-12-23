using Dashboard.Domain.Base.Commands;
using Dashboard.Domain.Base.Results;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsNone;

public record AddApiCredentialNone(string Name, string? Description = default) : ICommand<Result>;