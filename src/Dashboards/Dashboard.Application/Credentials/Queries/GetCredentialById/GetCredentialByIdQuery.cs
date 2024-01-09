using Dashboard.Domain.ValueObjects;
using SharedKernel.Base.Queries;
using SharedKernel.Entities;

namespace Dashboard.Application.Credentials.Queries.GetCredentialById;

public record GetCredentialByIdQuery(IdColumn Id) : IQuery<Credential>;