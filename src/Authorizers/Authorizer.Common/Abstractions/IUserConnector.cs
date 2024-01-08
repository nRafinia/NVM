using Authorizer.Common.Models;

namespace Authorizer.Common.Abstractions;

public interface IUserConnector
{
    List<UserInfo> GetUsers(IConfiguration configuration);
    UserInfo SignIn(IConfiguration configuration, string userName, string password);
}