using Dashboard.Domain.Base.Commands;
using Dashboard.Domain.Base.Results;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsBasic;

public record AddApiCredentialBasic(string Name, string UserName, string Password, string? Description = default)
    : ICommand<Result>;