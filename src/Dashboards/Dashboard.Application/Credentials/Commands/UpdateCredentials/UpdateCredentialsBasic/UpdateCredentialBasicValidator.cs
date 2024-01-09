using Dashboard.Application.Extensions;
using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

public class UpdateCredentialBasicValidator : RequestValidator<UpdateCredentialBasic>
{
    public UpdateCredentialBasicValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x).OneOfPropertiesMustNotEmpty();
    }
    
    
}