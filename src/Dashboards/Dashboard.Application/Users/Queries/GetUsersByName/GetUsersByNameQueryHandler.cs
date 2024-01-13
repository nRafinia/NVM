using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Entities;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetUsersByName;

public class GetUsersByNameQueryHandler(
    IUserRepository repository,
    ILogger<GetUsersByNameQueryHandler> logger)
    : IQueryHandler<GetUsersByNameQuery, List<User>>
{

    public async Task<Result<List<User>?>> Handle(GetUsersByNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetUsersByNameAsync(request.Name, cancellationToken);
            return Result.Success<List<User>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by name.");
            return e.ToResult<List<User>?>();
        }
    }
}