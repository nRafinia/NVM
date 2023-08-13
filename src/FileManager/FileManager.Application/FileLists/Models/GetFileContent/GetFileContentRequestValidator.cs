using FluentValidation;

namespace FileManager.Application.FileLists.Models.GetFileContent;

public class GetFileContentRequestValidator:AbstractValidator<GetFileContentRequest>
{
    public GetFileContentRequestValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
        RuleFor(v => v.Path).NotEmpty().NotEqual(".").NotEqual("..");
    }
}