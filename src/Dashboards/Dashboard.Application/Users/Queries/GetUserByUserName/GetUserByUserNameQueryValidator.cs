using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQueryValidator: RequestValidator<GetUserByUserNameQuery>
{
    public GetUserByUserNameQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}