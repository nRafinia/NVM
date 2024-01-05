using FluentValidation;

namespace Dashboard.Application.Credentials.Queries.GetCredentialByName;

public class GetCredentialByNameValidator:RequestValidator<GetCredentialByNameQuery>
{
    public GetCredentialByNameValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}