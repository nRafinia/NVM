using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetLocalUsers;

public class GetLocalUsersQueryValidator : RequestValidator<GetLocalUsersQuery>
{
    public GetLocalUsersQueryValidator()
    {
        RuleFor(x => x.LdapId).NotEmpty().NotEqual(IdColumn.None);
    }
}