using Authorizer.Common.Models;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Errors;
using SharedKernel.Extensions;

namespace Authorizer.Local.Application.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQueryHandler(
    IUserRepository repository,
    ILogger<GetUserByUserNameQueryHandler> logger) : IQueryHandler<GetUserByUserNameQuery, UserInfo>
{
    public async Task<Result<UserInfo?>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(u => u.UserName == request.UserName, cancellationToken);
            return user is null
                ? Result.Failure<UserInfo>(SharedErrors.ItemNotFound)
                : user.Adapt<UserInfo>();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by userName.");
            return e.ToResult<UserInfo>();
        }
    }
}