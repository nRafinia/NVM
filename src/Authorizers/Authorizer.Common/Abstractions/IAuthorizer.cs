using Authorizer.Common.Models;

namespace Authorizer.Common.Abstractions;

public interface IAuthorizer
{
    List<UserInfo> GetUsers();
    UserInfo SignIn(string userName, string password);
}