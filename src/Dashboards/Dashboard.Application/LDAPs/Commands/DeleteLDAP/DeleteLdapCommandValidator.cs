using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Commands.DeleteLDAP;

public class DeleteLdapCommandValidator : RequestValidator<DeleteLdapCommand>
{
    public DeleteLdapCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.None);
    }
}