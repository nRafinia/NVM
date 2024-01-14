using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.LDAPs.Queries.GetAllLDAPs;

public record GetAllLdapsQuery() : IQuery<List<LDAP>>;