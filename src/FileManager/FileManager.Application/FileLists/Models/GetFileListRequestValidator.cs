using FluentValidation;

namespace FileManager.Application.FileLists.Models;

public class GetFileListRequestValidator:AbstractValidator<GetFileListRequest>
{
    public GetFileListRequestValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
    }
}