using Dashboard.Domain.Entities.Users;
using Mapster;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(
    IUserRepository repository,
    ILogger<GetAllUsersQueryHandler> logger) : IQueryHandler<GetAllUsersQuery, List<User>>
{
    public async Task<Result<List<User>?>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await repository.GetAllAsync(cancellationToken);
            return users
                .Select(u => u.Adapt<User>())
                .ToList();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by id.");
            return e.ToResult<List<User>>();
        }
    }
}