using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQueryValidator: RequestValidator<GetUserByUserNameQuery>
{
    public GetUserByUserNameQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}