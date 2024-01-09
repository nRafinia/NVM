using SharedKernel.Abstractions;
using SharedKernel.Entities;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface ICredentialRepository : IBaseRepository<Credential>
{
    Task<IReadOnlyList<Credential>> GetAsync(string name);
    Task SaveChanges();
}