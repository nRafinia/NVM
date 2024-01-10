using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Commands.SetPassword;

public record SetPasswordCommand(IdColumn Id, string Password):ICommand;