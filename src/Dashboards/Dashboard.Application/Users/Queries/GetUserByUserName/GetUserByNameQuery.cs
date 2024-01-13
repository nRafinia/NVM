using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Users.Queries.GetUserByUserName;

public record GetUserByUserNameQuery(string UserName) : IQuery<User>;