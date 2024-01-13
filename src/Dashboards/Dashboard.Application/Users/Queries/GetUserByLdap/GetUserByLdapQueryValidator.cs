using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUserByLdap;

public class GetUserByLdapQueryValidator : RequestValidator<GetUserByLdapQuery>
{
    public GetUserByLdapQueryValidator()
    {
        RuleFor(x => x.LdapId).NotEmpty().NotEqual(IdColumn.None);
    }
}