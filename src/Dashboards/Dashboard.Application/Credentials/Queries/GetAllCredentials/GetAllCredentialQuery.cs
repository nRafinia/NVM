using SharedKernel.Base.Queries;
using SharedKernel.Entities;

namespace Dashboard.Application.Credentials.Queries.GetAllCredentials;

public record GetAllCredentialQuery() : IQuery<IReadOnlyList<Credential>>;