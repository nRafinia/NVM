using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPById;

public class GetLdapByIdQueryHandler(
    ILdapRepository repository,
    ILogger<GetLdapByIdQueryHandler> logger) : IQueryHandler<GetLdapByIdQuery, LDAP>
{
    public async Task<Result<LDAP?>> Handle(GetLdapByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);
            return user ?? Result.Failure<LDAP>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get LDAP by id.");
            return e.ToResult<LDAP>();
        }
    }
}