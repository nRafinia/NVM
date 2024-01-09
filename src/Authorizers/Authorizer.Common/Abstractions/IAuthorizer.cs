using Authorizer.Common.Models;
using SharedKernel.Base.Results;
using SharedKernel.Entities;

namespace Authorizer.Common.Abstractions;

public interface IAuthorizer
{
    Result<List<UserInfo>?> GetUsers(Credential credential);
    Result<UserInfo?> SignIn(string userName, string password);
}