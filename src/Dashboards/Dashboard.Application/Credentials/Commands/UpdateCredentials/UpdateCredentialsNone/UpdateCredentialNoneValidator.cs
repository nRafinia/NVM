using Dashboard.Application.Extensions;
using FluentValidation;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;

public class UpdateCredentialNoneValidator : RequestValidator<UpdateCredentialNone>
{
    public UpdateCredentialNoneValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x).OneOfPropertiesMustNotEmpty();
    }
}