using Authorizer.Common.Models;
using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Queries;

namespace Authorizer.Local.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(IdColumn Id) : IQuery<UserInfo>;