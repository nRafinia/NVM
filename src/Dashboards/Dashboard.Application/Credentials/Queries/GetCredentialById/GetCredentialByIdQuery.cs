using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Queries;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public record GetCredentialByIdQuery(IdColumn Id) : IQuery<Credential>;