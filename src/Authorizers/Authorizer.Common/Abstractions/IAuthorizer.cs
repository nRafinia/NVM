using Authorizer.Common.Models;
using SharedKernel.Base.Results;

namespace Authorizer.Common.Abstractions;

public interface IAuthorizer
{
    Task<Result<List<UserInfo>?>> GetUsers();
    Task<Result<UserInfo?>> SignIn(string userName, string password);
}