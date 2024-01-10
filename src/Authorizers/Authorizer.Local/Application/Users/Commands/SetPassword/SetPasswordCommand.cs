using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Authorizer.Local.Application.Users.Commands.SetPassword;

public record SetPasswordCommand(IdColumn Id, string Password):ICommand;