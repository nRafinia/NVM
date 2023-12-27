using Dashboard.Application.Extensions;
using FluentValidation;

namespace Dashboard.Application.Credentials.UpdateCredentials.UpdateCredentialsNone;

public class UpdateCredentialNoneValidator : AbstractValidator<UpdateCredentialNone>
{
    public UpdateCredentialNoneValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x).OneOfPropertiesMustNotEmpty();
    }
}