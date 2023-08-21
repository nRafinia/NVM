using FluentValidation;

namespace FileManager.Application.FileLists.Command.DeleteFolder;

public class DeleteFolderCommandValidator : AbstractValidator<DeleteFolderCommand>
{
    public DeleteFolderCommandValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
    }
}