using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public record DeleteCredentialCommand(IdColumn Id) : ICommand;