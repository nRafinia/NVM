using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetLocalUsers;

public record GetLocalUsersQuery(IdColumn LdapId) : IQuery<List<User>>;