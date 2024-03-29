using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IUserRepository repository,
    ILogger<GetUserByIdQueryHandler> logger) : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);
            return user ?? Result.Failure<User>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by id.");
            return e.ToResult<User>();
        }
    }
}