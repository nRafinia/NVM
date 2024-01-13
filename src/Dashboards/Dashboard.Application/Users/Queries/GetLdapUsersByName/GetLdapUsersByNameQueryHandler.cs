using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetLdapUsersByName;

public class GetLdapUsersByNameQueryHandler(
    IUserRepository repository,
    ILogger<GetLdapUsersByNameQueryHandler> logger)
    : IQueryHandler<GetLdapUsersByNameQuery, List<User>>
{

    public async Task<Result<List<User>?>> Handle(GetLdapUsersByNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetLdapUsersByNameAsync(request.ldapId, request.Name, cancellationToken);
            return Result.Success<List<User>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get LDAP user by name.");
            return e.ToResult<List<User>?>();
        }
    }
}