using System.Reflection.Metadata.Ecma335;
using Dashboard.Domain.Entities.Users;
using Mapster;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetUserByLdap;

public class GetUserByLdapQueryHandler(
    IUserRepository repository,
    ILogger<GetUserByLdapQueryHandler> logger) : IQueryHandler<GetUserByLdapQuery, List<User>>
{
    public async Task<Result<List<User>?>> Handle(GetUserByLdapQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await repository.GetUsersByLdapAsync(request.LdapId, cancellationToken);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by id.");
            return e.ToResult<List<User>>();
        }
    }
}