using Authorizer.Local.Domain.Entities;
using SharedKernel.Abstractions;

namespace Authorizer.Local.Persistence.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}