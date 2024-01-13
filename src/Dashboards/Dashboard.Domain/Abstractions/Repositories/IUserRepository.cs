using Dashboard.Domain.Entities.Users;
using SharedKernel.Abstractions;
using SharedKernel.ValueObjects;

namespace Dashboard.Domain.Abstractions.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsExistUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<List<User>> GetUsersByLdapAsync(IdColumn ldapId, CancellationToken cancellationToken = default);
    Task<List<User>> GetLocalUsersAsync(CancellationToken cancellationToken = default);
}