using FileManager.Application.FileLists.Query.GetFileContent;
using FluentValidation;

namespace FileManager.Application.FileLists.Query.GetTextFileContent;

public sealed class GetTextFileContentQueryValidator:AbstractValidator<GetTextFileContentQuery>
{
    public GetTextFileContentQueryValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
    }
}