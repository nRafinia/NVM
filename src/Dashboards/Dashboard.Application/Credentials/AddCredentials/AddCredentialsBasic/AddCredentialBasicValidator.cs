using FluentValidation;

namespace Dashboard.Application.Credentials.AddCredentials.AddCredentialsBasic;

public class AddCredentialBasicValidator : AbstractValidator<AddCredentialBasic>
{
    public AddCredentialBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}