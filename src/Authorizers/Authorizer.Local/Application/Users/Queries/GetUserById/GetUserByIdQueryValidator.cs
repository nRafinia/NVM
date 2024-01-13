using FluentValidation;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryValidator : RequestValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.None);
    }
}