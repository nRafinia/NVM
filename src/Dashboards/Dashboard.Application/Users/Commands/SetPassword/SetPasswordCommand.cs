using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.SetPassword;

public record SetPasswordCommand(IdColumn Id, string Password):ICommand;