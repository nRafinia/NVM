using Authorizer.Common.Models;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Errors;
using SharedKernel.Extensions;

namespace Authorizer.Local.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IUserRepository repository,
    ILogger<GetUserByIdQueryHandler> logger) : IQueryHandler<GetUserByIdQuery, UserInfo>
{
    public async Task<Result<UserInfo?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);
            return user is null
                ? Result.Failure<UserInfo>(SharedErrors.ItemNotFound)
                : user.Adapt<UserInfo>();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in set password.");
            return e.ToResult<UserInfo>();
        }
    }
}