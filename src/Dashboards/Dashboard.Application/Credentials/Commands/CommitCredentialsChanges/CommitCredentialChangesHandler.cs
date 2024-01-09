using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;

namespace Dashboard.Application.Credentials.Commands.CommitCredentialsChanges;

public class CommitCredentialChangesHandler(ICredentialRepository repository):ICommandHandler<CommitCredentialChanges>
{
    public async Task<Result> Handle(CommitCredentialChanges request, CancellationToken cancellationToken)
    {
        await repository.SaveChanges();
        return Result.Success();
    }
}