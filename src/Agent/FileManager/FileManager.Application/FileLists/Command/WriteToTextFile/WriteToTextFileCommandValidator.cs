using FluentValidation;

namespace FileManager.Application.FileLists.Command.WriteToTextFile;

public sealed class WriteToTextFileCommandValidator : AbstractValidator<WriteToTextFileCommand>
{
    public WriteToTextFileCommandValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
        RuleFor(v => v.Content).NotEmpty();
    }
}