using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetLdapUsers;

public record GetLdapUsersQuery(IdColumn ldapId) : IQuery<List<User>>;