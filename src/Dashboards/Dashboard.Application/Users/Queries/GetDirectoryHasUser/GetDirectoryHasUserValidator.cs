using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetDirectoryHasUser;

public class GetDirectoryHasUserValidator:RequestValidator<GetDirectoryHasUserQuery>
{
    public GetDirectoryHasUserValidator()
    {
        RuleFor(v => v.LdapId).NotEmpty().NotEqual(IdColumn.None);
    }
}