using Dashboard.Domain.Entities;
using SharedKernel.Abstractions;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface ICredentialRepository : IBaseRepository<Credential>
{
    Task<IReadOnlyList<Credential>> GetAsync(string name);
    Task SaveChanges();
}