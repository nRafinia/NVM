using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryValidator : RequestValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.None);
    }
}