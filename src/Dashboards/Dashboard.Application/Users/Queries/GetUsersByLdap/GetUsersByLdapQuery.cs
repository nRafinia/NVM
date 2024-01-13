using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUsersByLdap;

public record GetUsersByLdapQuery(IdColumn LdapId) : IQuery<List<User>>;