using FluentValidation;

namespace FileManager.Application.FileLists.Command.WriteToBinaryFile;

public sealed class WriteToBinaryFileCommandValidator : AbstractValidator<WriteToBinaryFileCommand>
{
    public WriteToBinaryFileCommandValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
        RuleFor(v => v.Content).NotEmpty();
    }
}