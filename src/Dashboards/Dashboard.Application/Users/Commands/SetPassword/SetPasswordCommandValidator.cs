using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.SetPassword;

public class SetPasswordCommandValidator : RequestValidator<SetPasswordCommand>
{
    public SetPasswordCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.None);
        RuleFor(x => x.Password).NotEmpty();
    }
}