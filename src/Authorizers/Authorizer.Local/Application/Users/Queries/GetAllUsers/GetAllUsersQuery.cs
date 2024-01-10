using Authorizer.Common.Models;
using SharedKernel.Base.Queries;

namespace Authorizer.Local.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<List<UserInfo>>;