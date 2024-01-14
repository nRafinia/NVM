using Dashboard.Application.Users.Models;
using Dashboard.Domain.Entities.Users;

namespace Dashboard.Application.Users;

public interface IUserService
{
    Task<User?> AuthenticateAsync(LoginRequest model);
    Task<User?> GetProfileAsync(GetProfileRequest model);
}