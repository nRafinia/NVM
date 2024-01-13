using Dashboard.Domain.Entities.Users;
using Mapster;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetUserByUserName;

public class GetUserByUserNameQueryHandler(
    IUserRepository repository,
    ILogger<GetUserByUserNameQueryHandler> logger) : IQueryHandler<GetUserByUserNameQuery, User>
{
    public async Task<Result<User?>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetByUserNameAsync(request.UserName, cancellationToken);
            return user is null
                ? Result.Failure<User>(SharedErrors.ItemNotFound)
                : user.Adapt<User>();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by userName.");
            return e.ToResult<User>();
        }
    }
}