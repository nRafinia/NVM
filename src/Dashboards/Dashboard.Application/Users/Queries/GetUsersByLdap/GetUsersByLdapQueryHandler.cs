using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetUsersByLdap;

public class GetUsersByLdapQueryHandler(
    IUserRepository repository,
    ILogger<GetUsersByLdapQueryHandler> logger) : IQueryHandler<GetUsersByLdapQuery, List<User>>
{
    public async Task<Result<List<User>?>> Handle(GetUsersByLdapQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await repository.GetUsersByLdapAsync(request.LdapId, cancellationToken);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get user by LDAP id.");
            return e.ToResult<List<User>>();
        }
    }
}