using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(IdColumn Id, string DisplayName) : ICommand<User>;