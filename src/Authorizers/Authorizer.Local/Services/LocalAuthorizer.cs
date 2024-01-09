using Authorizer.Common.Abstractions;
using Authorizer.Common.Models;
using SharedKernel.Base.Results;

namespace Authorizer.Local.Services;

public class LocalAuthorizer() : IAuthorizer
{
    public Task<Result<List<UserInfo>?>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserInfo?>> SignIn(string userName, string password)
    {
        throw new NotImplementedException();
    }
}