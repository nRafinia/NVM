using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUsersByLdap;

public class GetUsersByLdapQueryValidator : RequestValidator<GetUsersByLdapQuery>
{
    public GetUsersByLdapQueryValidator()
    {
        RuleFor(x => x.LdapId).NotEmpty().NotEqual(IdColumn.None);
    }
}