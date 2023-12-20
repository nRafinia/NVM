using Dashboard.Domain.Entities;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface ICredentialRepository
{
    Task AddAsync(Credential credential, CancellationToken cancellationToken = default);
    Task UpdateAsync(Credential credential, CancellationToken cancellationToken = default);
    Task<Credential?> GetAsync(IdColumn id, CancellationToken cancellationToken = default);
    Task<Credential?> GetAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Credential>> GetAllAsync(CancellationToken cancellationToken = default);
    Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default);
}