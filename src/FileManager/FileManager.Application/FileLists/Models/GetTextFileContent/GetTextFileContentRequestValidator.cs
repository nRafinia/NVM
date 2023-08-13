using FileManager.Application.FileLists.Models.GetFileContent;
using FluentValidation;

namespace FileManager.Application.FileLists.Models.GetTextFileContent;

public class GetTextFileContentRequestValidator:AbstractValidator<GetFileContentRequest>
{
    public GetTextFileContentRequestValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
    }
}