using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator:RequestValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().NotEqual(IdColumn.New);
        RuleFor(u => u.DisplayName).NotEmpty();
    }
}