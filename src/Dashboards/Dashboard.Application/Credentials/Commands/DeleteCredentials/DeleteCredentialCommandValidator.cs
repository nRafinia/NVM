using FluentValidation;

namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public class DeleteCredentialCommandValidator : AbstractValidator<DeleteCredentialCommand>
{
    public DeleteCredentialCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}