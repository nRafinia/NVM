using SharedKernel.Base.Commands;

namespace Dashboard.Application.Users.Commands.AddUser;

public record AddUserCommand(string UserName, string Password, string DisplayName) : ICommand;