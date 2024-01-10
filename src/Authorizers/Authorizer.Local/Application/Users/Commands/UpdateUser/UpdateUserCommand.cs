using Authorizer.Common.Models;
using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Commands;

namespace Authorizer.Local.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(IdColumn Id, string DisplayName) : ICommand<UserInfo>;