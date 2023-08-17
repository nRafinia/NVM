using FluentValidation;

namespace FileManager.Application.FileLists.Query.GetFileList;

public sealed class GetFileListQueryValidator : AbstractValidator<GetFileListQuery>
{
    public GetFileListQueryValidator()
    {
        RuleFor(v => v.Root).NotEmpty();
    }
}