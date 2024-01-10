using Authorizer.Common.Models;
using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(IdColumn Id, string DisplayName) : ICommand<UserInfo>;