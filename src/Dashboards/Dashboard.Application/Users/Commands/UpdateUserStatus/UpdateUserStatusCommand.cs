using Dashboard.Domain.Entities.Users;
using Dashboard.Domain.Entities.Users.Enums;
using SharedKernel.Base.Commands;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand(IdColumn Id, UserStatus Status) : ICommand;