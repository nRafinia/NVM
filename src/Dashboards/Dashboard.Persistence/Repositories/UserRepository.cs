using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities.Users;
using Dashboard.Domain.Entities.Users.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using SharedKernel.Persistence.Base;
using SharedKernel.ValueObjects;

namespace Dashboard.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context)
    : BaseRepository<ApplicationDbContext, User>(context), IUserRepository
{
    public Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return DbSet.Where(u => u.UserName == userName).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsExistUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(u => u.UserName == userName, cancellationToken);
    }

    public Task<List<User>> GetUsersByLdapAsync(IdColumn ldapId, CancellationToken cancellationToken = default)
    {
        return DbSet
            .Where(u => u.AuthorizerType == AuthorizerType.LDAP && u.Ldap!.Id == ldapId)
            .ToListAsync(cancellationToken);
    }

    public Task<List<User>> GetLocalUsersAsync(CancellationToken cancellationToken = default)
    {
        return DbSet
            .Where(u => u.AuthorizerType == AuthorizerType.Local)
            .ToListAsync(cancellationToken);
    }

    public Task<List<User>> GetUsersByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return DbSet
            .Where(u => u.UserName.Contains(name) || u.DisplayName.Contains(name))
            .ToListAsync(cancellationToken);
    }
}