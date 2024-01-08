using Authorizer.Common.Abstractions;
using Authorizer.Common.Models;
using Authorizer.Ldap.Models;

namespace Authorizer.Ldap.Services;

public class LdapAuthorizer(LdapConfiguration configuration) : IAuthorizer
{
    public List<UserInfo> GetUsers()
    {
        throw new NotImplementedException();
    }

    public UserInfo SignIn(string userName, string password)
    {
        throw new NotImplementedException();
    }
}