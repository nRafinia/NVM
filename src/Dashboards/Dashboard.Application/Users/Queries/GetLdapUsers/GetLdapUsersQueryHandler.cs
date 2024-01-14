using Dashboard.Application.Users.Queries.GetUsersByLdap;
using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetLdapUsers;

public class GetLdapUsersQueryHandler(
    IUserRepository repository,
    ILogger<GetUsersByLdapQueryHandler> logger) : IQueryHandler<GetLdapUsersQuery, List<User>>
{
    public async Task<Result<List<User>?>> Handle(GetLdapUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await repository.GetUsersByLdapAsync(request.ldapId, cancellationToken);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get local users.");
            return e.ToResult<List<User>>();
        }
    }
}