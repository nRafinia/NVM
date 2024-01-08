using Dashboard.Application.Users.Models;

namespace Dashboard.Application.Users;

public interface IUserService
{
    Task<User?> AuthenticateAsync(LoginRequest model);
    Task<User?> GetProfileAsync(GetProfileRequest model);
}