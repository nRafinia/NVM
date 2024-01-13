using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Users.Queries.GetLocalUsers;

public record GetLocalUsersQuery() : IQuery<List<User>>;