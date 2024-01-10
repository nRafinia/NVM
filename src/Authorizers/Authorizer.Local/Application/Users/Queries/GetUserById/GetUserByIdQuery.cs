using Authorizer.Common.Models;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Authorizer.Local.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(IdColumn Id) : IQuery<UserInfo>;