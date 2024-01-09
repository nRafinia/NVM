using FluentValidation;
using SharedKernel.Base;

namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public class DeleteCredentialCommandValidator : RequestValidator<DeleteCredentialCommand>
{
    public DeleteCredentialCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}