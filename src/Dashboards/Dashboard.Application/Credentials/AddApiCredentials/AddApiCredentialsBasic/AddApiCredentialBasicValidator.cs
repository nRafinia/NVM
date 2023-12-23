using FluentValidation;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsBasic;

public class AddApiCredentialBasicValidator : AbstractValidator<AddApiCredentialBasic>
{
    public AddApiCredentialBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}