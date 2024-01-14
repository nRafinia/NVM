using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<List<User>>;