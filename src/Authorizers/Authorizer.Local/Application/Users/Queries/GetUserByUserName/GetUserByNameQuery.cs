using Authorizer.Common.Models;
using SharedKernel.Base.Queries;

namespace Authorizer.Local.Application.Users.Queries.GetUserByUserName;

public record GetUserByUserNameQuery(string UserName) : IQuery<UserInfo>;