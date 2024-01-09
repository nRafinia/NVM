using Authorizer.Common.Models;
using SharedKernel.Base.Results;

namespace Authorizer.Common.Abstractions;

public interface IAuthorizer
{
    Result<List<UserInfo>?> GetUsers();
    Result<UserInfo?> SignIn(string userName, string password);
}