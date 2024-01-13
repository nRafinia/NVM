using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Users.Queries.GetUsersByName;

public record GetUsersByNameQuery(string Name) : IQuery<List<User>>;