using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.LDAPs.Queries.GetLDAPById;

public record GetLdapByIdQuery(IdColumn Id) : IQuery<LDAP>;