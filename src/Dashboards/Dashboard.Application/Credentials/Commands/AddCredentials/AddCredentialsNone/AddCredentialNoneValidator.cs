using FluentValidation;

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;

public class AddCredentialNoneValidator : AbstractValidator<AddCredentialNone>
{
    public AddCredentialNoneValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}