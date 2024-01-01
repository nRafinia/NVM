using Dashboard.Application.Extensions;
using FluentValidation;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

public class UpdateCredentialBasicValidator : AbstractValidator<UpdateCredentialBasic>
{
    public UpdateCredentialBasicValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x).OneOfPropertiesMustNotEmpty();
    }
    
    
}