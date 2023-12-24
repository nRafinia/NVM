using Dashboard.Domain.Entities;

namespace Dashboard.Domain.Abstractions;

public interface IUserProvider
{
    IdColumn GetCurrentUserId();
}