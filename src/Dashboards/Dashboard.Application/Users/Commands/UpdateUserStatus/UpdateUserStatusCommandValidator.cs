using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusCommandValidator:RequestValidator<UpdateUserStatusCommand>
{
    public UpdateUserStatusCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty().NotEqual(IdColumn.None);
    }
}