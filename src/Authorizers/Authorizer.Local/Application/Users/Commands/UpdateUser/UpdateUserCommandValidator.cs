using Dashboard.Domain.ValueObjects;
using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator:RequestValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().NotEqual(IdColumn.New);
        RuleFor(u => u.DisplayName).NotEmpty();
    }
}