using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(IdColumn Id, string OldPassword, string NewPassword) : ICommand;