using Dashboard.Application.Users.Models;
using Dashboard.Domain.Entities;

namespace Dashboard.Application.Users;

public interface IUserLogic
{
    Task<User?> AuthenticateAsync(LoginRequest model);
    Task<User?> GetProfileAsync(GetProfileRequest model);
}