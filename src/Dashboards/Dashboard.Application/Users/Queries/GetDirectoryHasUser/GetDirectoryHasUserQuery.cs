using Dashboard.Domain.Entities.Users.Enums;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetDirectoryHasUser;

public record GetDirectoryHasUserQuery(IdColumn LdapId, AuthorizerType Type) : IQuery<bool>;