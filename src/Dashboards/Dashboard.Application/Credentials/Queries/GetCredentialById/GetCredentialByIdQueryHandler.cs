using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public class GetCredentialByIdQueryHandler(
    ICredentialRepository repository,
    ILogger<GetCredentialByIdQueryHandler> logger)
    : IQueryHandler<GetCredentialByIdQuery, Credential>
{
    public async Task<Result<Credential?>> Handle(GetCredentialByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetAsync(request.Id);
            return result ?? Result.Failure<Credential>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add none credential.");
            return e.ToResult<Credential>();
        }
    }
}