using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities.LDAPs;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using SharedKernel.Persistence.Base;

namespace Dashboard.Persistence.Repositories;

public class LdapRepository(ApplicationDbContext context)
    : BaseRepository<ApplicationDbContext, LDAP>(context), ILdapRepository
{
    public Task<bool> IsExistNameAsync(string name, CancellationToken cancellationToken)
    {
        return DbSet.AnyAsync(l => l.Name == name, cancellationToken);
    }

    public async Task<List<LDAP>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(l => l.Name.Contains(name))
            .ToListAsync(cancellationToken);
    }
}