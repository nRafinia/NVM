using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Credentials.Queries.GetAllCredentials;

public class GetAllCredentialQueryHandler(
    ICredentialRepository repository,
    ILogger<GetAllCredentialQueryHandler> logger)
    : IQueryHandler<GetAllCredentialQuery, IReadOnlyList<Credential>>
{
    public async Task<Result<IReadOnlyList<Credential>?>> Handle(GetAllCredentialQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetAllAsync();
            return Result.Success<IReadOnlyList<Credential>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add none credential.");
            return e.ToResult<IReadOnlyList<Credential>>();
        }
    }
}