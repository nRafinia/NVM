using FluentValidation;

namespace FileManager.Application.FileLists.Query.GetFileContent;

public sealed class GetFileContentQueryValidator : AbstractValidator<GetFileContentQuery>
{
    public GetFileContentQueryValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
    }
}