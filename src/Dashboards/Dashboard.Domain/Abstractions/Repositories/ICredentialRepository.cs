using Dashboard.Domain.Entities;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface ICredentialRepository
{
    Task<Credential> AddAsync(Credential credential);
    Task<Credential> UpdateAsync(Credential credential);
    Task<Credential?> GetAsync(IdColumn id);
    Task<Credential?> GetAsync(string name);
    Task<IReadOnlyCollection<Credential>> GetAllAsync();
    Task DeleteAsync(IdColumn id);
}