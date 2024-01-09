using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public record DeleteCredentialCommand(IdColumn Id) : ICommand;