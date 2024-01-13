using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(IdColumn Id) : IQuery<User>;