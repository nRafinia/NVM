using Authorizer.Local.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using SharedKernel.Persistence.Base;

namespace Authorizer.Local.Persistence.Repositories;

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
}