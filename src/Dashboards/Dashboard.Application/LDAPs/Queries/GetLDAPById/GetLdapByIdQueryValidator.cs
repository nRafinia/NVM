using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPById;

public class GetLdapByIdQueryValidator : RequestValidator<GetLdapByIdQuery>
{
    public GetLdapByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.None);
    }
}