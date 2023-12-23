using FluentValidation;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsNone;

public class AddApiCredentialNoneValidator : AbstractValidator<AddApiCredentialNone>
{
    public AddApiCredentialNoneValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}