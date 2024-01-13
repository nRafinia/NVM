using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Abstractions;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface ILdapRepository : IBaseRepository<LDAP>
{
    Task<bool> IsExistNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<LDAP>> GetByNameAsync(string name, CancellationToken cancellationToken);
}