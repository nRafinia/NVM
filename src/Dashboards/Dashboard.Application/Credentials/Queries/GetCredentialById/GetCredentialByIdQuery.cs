using SharedKernel.Base.Queries;
using SharedKernel.Entities;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public record GetCredentialByIdQuery(IdColumn Id) : IQuery<Credential>;