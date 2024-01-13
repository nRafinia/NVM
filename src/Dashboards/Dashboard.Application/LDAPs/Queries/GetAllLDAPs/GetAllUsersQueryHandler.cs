using Dashboard.Domain.Entities.LDAPs;
using Mapster;
using SharedKernel.Base.Queries;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Queries.GetAllLDAPs;

public class GetAllLdapsQueryHandler(
    ILdapRepository repository,
    ILogger<GetAllLdapsQueryHandler> logger) : IQueryHandler<GetAllLdapsQuery, List<LDAP>>
{
    public async Task<Result<List<LDAP>?>> Handle(GetAllLdapsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await repository.GetAllAsync(cancellationToken);
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in get all LDAPs.");
            return e.ToResult<List<LDAP>>();
        }
    }
}