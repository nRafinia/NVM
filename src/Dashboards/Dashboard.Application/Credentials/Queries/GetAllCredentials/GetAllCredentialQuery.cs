using Dashboard.Domain.Base.Queries;

namespace Dashboard.Application.Credentials.Queries.GetAllCredentials;

public record GetAllCredentialQuery() : IQuery<IReadOnlyList<Credential>>;