using SharedKernel.Base.Queries;

namespace Dashboard.Application.Credentials.Queries.GetCredentialByName;

public record GetCredentialByNameQuery(string Name) : IQuery<IReadOnlyList<Credential>>;