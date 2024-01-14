using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Entities;
using SharedKernel.Extensions;

namespace Dashboard.Application.Credentials.Queries.GetCredentialByName;

public class GetCredentialByNameQueryHandler(
    ICredentialRepository repository,
    ILogger<GetCredentialByNameQueryHandler> logger)
    : IQueryHandler<GetCredentialByNameQuery, IReadOnlyList<Credential>>
{
    public async Task<Result<IReadOnlyList<Credential>?>> Handle(GetCredentialByNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetAsync(request.Name);
            return Result.Success<IReadOnlyList<Credential>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add none credential.");
            return e.ToResult<IReadOnlyList<Credential>?>();
        }
    }
}