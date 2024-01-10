using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Authorizer.Local.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(IdColumn Id, string OldPassword, string NewPassword) : ICommand;