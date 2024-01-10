using Authorizer.Common.Models;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Authorizer.Local.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(
    IUserRepository repository,
    ILogger<GetAllUsersQueryHandler> logger) : IQueryHandler<GetAllUsersQuery, List<UserInfo>>
{
    public async Task<Result<List<UserInfo>?>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await repository.GetAllAsync(cancellationToken);
            return users
                .Select(u => u.Adapt<UserInfo>())
                .ToList();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by id.");
            return e.ToResult<List<UserInfo>>();
        }
    }
}