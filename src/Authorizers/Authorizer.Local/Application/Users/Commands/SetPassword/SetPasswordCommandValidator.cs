using Dashboard.Domain.ValueObjects;
using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Commands.SetPassword;

public class SetPasswordCommandValidator : RequestValidator<SetPasswordCommand>
{
    public SetPasswordCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.New);
        RuleFor(x => x.Password).NotEmpty();
    }
}