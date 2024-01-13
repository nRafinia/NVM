using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Users.Queries.GetUsersByName;

public class GetUsersByNameQueryValidator:RequestValidator<GetUsersByNameQuery>
{
    public GetUsersByNameQueryValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}