using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetLdapUsersByName;

public record GetLdapUsersByNameQuery(IdColumn ldapId, string Name) : IQuery<List<User>>;