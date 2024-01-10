using Dashboard.Domain.ValueObjects;
using FluentValidation;
using SharedKernel.Base;

namespace Authorizer.Local.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryValidator : RequestValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(IdColumn.New);
    }
}