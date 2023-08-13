using FluentValidation;

namespace FileManager.Application.FileLists.Models.GetFileList;

public class GetFileListRequestValidator : AbstractValidator<GetFileListRequest>
{
    public GetFileListRequestValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
    }
}