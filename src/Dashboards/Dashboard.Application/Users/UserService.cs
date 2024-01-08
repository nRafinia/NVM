using Dashboard.Application.Users.Models;
using Dashboard.Domain.Entities;

namespace Dashboard.Application.Users;

public class UserService : IUserService
{
    public async Task<User?> AuthenticateAsync(LoginRequest model)
    {
        /*var user = await _userRepository.GetByUserNameAsync(model.UserName);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password != model.Password)
        {
            throw new Exception("Invalid password");
        }

        return user;*/
        return new User("1", "test", "test", "1");
    }

    public async Task<User?> GetProfileAsync(GetProfileRequest model)
    {
        return new User("1", "test", "test", "1");
    }
}