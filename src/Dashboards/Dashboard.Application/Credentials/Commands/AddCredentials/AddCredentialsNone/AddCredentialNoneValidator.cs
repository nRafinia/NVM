using FluentValidation;

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;

public class AddCredentialNoneValidator : RequestValidator<AddCredentialNone>
{
    public AddCredentialNoneValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}