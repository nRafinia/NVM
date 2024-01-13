using Dashboard.Domain.Entities.Users;
using SharedKernel.Base.Queries;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Users.Queries.GetUserByLdap;

public record GetUserByLdapQuery(IdColumn LdapId) : IQuery<List<User>>;