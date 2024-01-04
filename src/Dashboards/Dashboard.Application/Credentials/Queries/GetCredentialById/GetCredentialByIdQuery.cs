using Dashboard.Domain.Base.Queries;
using Dashboard.Domain.ValueObjects;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public record GetCredentialByIdQuery(IdColumn Id) : IQuery<Credential>;