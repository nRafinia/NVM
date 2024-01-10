using Dashboard.Domain.ValueObjects;
using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandValidator : RequestValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.New);
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}