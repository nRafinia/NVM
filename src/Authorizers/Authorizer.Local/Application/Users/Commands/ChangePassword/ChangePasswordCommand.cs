using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(IdColumn Id, string OldPassword, string NewPassword) : ICommand;