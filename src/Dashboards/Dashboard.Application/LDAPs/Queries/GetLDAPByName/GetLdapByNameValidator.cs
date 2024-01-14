using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPByName;

public class GetLdapByNameValidator : RequestValidator<GetLDAPByNameQuery>
{
    public GetLdapByNameValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}