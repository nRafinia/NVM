using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Users.Queries.GetLocalUsersByName;

public record GetLocalUsersByNameQuery(string Name) : IQuery<List<User>>;