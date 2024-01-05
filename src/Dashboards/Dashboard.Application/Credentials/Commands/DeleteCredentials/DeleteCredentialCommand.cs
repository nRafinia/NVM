using Dashboard.Domain.ValueObjects;

namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public record DeleteCredentialCommand(IdColumn Id) : ICommand;