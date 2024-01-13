using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetLdapUsersByName;

public class GetLdapUsersByNameQueryValidator:RequestValidator<GetLdapUsersByNameQuery>
{
    public GetLdapUsersByNameQueryValidator()
    {
        RuleFor(v => v.ldapId).NotEmpty().NotEqual(IdColumn.None);
        RuleFor(v => v.Name).NotEmpty();
    }
}