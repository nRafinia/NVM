using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetLocalUsersByName;

public class GetLocalUsersByNameQueryHandler(
    IUserRepository repository,
    ILogger<GetLocalUsersByNameQueryHandler> logger)
    : IQueryHandler<GetLocalUsersByNameQuery, List<User>>
{

    public async Task<Result<List<User>?>> Handle(GetLocalUsersByNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetLocalUsersByNameAsync(request.Name, cancellationToken);
            return Result.Success<List<User>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get local user by name.");
            return e.ToResult<List<User>?>();
        }
    }
}