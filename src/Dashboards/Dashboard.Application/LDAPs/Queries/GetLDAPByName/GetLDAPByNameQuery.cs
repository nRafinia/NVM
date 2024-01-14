using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPByName;

public record GetLDAPByNameQuery(string Name) : IQuery<List<LDAP>>;