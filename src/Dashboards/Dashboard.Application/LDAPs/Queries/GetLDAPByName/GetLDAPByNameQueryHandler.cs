using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPByName;

public class GetLDAPByNameQueryHandler(
    ILdapRepository repository,
    ILogger<GetLDAPByNameQueryHandler> logger)
    : IQueryHandler<GetLDAPByNameQuery, List<LDAP>>
{
    public async Task<Result<List<LDAP>?>> Handle(GetLDAPByNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetByNameAsync(request.Name, cancellationToken);
            return Result.Success<List<LDAP>?>(result);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add none credential.");
            return e.ToResult<List<LDAP>?>();
        }
    }
}