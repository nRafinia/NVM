using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Commands.AddLDAP;

public class AddLdapCommandValidator : RequestValidator<AddLdapCommand>
{
    public AddLdapCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Port).GreaterThanOrEqualTo(0);
        RuleFor(x => x.HostName).NotEmpty();
        RuleFor(x => x.CredentialId).NotEmpty().NotEqual(IdColumn.None);
        RuleFor(x => x.BaseDn).NotEmpty();
        RuleFor(x => x.FilterQuery).NotEmpty();
        RuleFor(x => x.Scope).NotEmpty();
        RuleFor(x => x.ProtocolVersion).GreaterThanOrEqualTo(0);
    }
}