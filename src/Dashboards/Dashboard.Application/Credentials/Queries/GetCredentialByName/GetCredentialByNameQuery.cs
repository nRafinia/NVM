using SharedKernel.Base.Queries;
using SharedKernel.Entities;

namespace Dashboard.Application.Credentials.Queries.GetCredentialByName;

public record GetCredentialByNameQuery(string Name) : IQuery<IReadOnlyList<Credential>>;