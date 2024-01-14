using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Queries.GetDirectoryHasUser;

public class GetDirectoryHasUserHandler(
    IUserRepository repository,
    ILogger<GetDirectoryHasUserHandler> logger) : IQueryHandler<GetDirectoryHasUserQuery, bool>
{
    public async Task<Result<bool>> Handle(GetDirectoryHasUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await repository
                .ExistsAsync(l => l.AuthorizerType == request.Type && l.Ldap!.Id == request.LdapId, cancellationToken);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get local users.");
            return e.ToResult<bool>();
        }
    }
}