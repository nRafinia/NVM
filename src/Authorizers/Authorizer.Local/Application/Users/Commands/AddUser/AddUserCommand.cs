using SharedKernel.Base.Commands;

namespace Authorizer.Local.Application.Users.Commands.AddUser;

public record AddUserCommand(string UserName, string Password, string DisplayName) : ICommand;