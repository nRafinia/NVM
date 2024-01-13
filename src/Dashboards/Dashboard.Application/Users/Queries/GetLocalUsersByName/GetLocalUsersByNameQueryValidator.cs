using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Users.Queries.GetLocalUsersByName;

public class GetLocalUsersByNameQueryValidator:RequestValidator<GetLocalUsersByNameQuery>
{
    public GetLocalUsersByNameQueryValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}