using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public class GetCredentialByIdValidator:RequestValidator<GetCredentialByIdQuery>
{
    public GetCredentialByIdValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}