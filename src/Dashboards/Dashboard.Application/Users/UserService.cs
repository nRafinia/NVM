using Authorizer.Ldap.Models;
using Authorizer.Ldap.Services;
using Dashboard.Application.LDAPs.Queries.GetLDAPById;
using Dashboard.Application.Users.Models;
using Dashboard.Application.Users.Queries.GetUserById;
using Dashboard.Application.Users.Queries.GetUserByUserName;
using Dashboard.Domain.Entities.Users;
using Mapster;
using MediatR;
using SharedKernel.Abstractions;
using AuthorizerType = Dashboard.Domain.Entities.Users.Enums.AuthorizerType;

namespace Dashboard.Application.Users;

public class UserService(IMediator mediator, ILogger<UserService> logger, ICredentialService credentialService)
    : IUserService
{
    public async Task<User?> AuthenticateAsync(LoginRequest model)
    {
        var getUserResponse = await mediator.Send(new GetUserByUserNameQuery(model.UserName));
        if (getUserResponse.IsFailure)
        {
            logger.LogError("Error in get user info, request={Username}, code={Code}, message={Message}",
                model.UserName, getUserResponse.Error!.Code, getUserResponse.Error!.Message);
            return null;
        }

        var user = getUserResponse.Value!;
        return user.AuthorizerType switch
        {
            AuthorizerType.Local => await LoginLocal(user, model.Password),
            AuthorizerType.LDAP => await LdapLogin(user, model.Password),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<User?> GetProfileAsync(GetProfileRequest model)
    {
        var getUserResponse = await mediator.Send(new GetUserByIdQuery(model.UserId));
        if (getUserResponse.IsFailure)
        {
            logger.LogError("Error in get user info, request={UserId}, code={Code}, message={Message}",
                model.UserId, getUserResponse.Error!.Code, getUserResponse.Error!.Message);
            return null;
        }

        var user = getUserResponse.Value!;
        return string.Equals(user.UserName, model.UserName, StringComparison.OrdinalIgnoreCase)
            ? user
            : null;
    }

    private Task<User?> LoginLocal(User user, string password)
    {
        var passwordIsCorrect = user.CheckPassword(password);
        if (!passwordIsCorrect)
        {
            logger.LogError("Password is incorrect, user={Username}", user.UserName);
            return Task.FromResult(default(User));
        }

        return Task.FromResult(user)!;
    }

    private async Task<User?> LdapLogin(User user, string password)
    {
        var getLdapResponse = await mediator.Send(new GetLdapByIdQuery(user.Ldap!.Id));
        if (getLdapResponse.IsFailure)
        {
            logger.LogError("Get ldap data is failed, user={Username}, code={Code}, message={Message}",
                user.UserName, getLdapResponse.Error!.Code, getLdapResponse.Error!.Message);
            return default;
        }

        var ldap = getLdapResponse.Value!;
        var configuration = ldap.Adapt<LdapConfiguration>();

        var authorizer = new LdapAuthorizer(configuration, credentialService);

        var loginResponse = await authorizer.SignIn(user.UserName, password);
        if (!loginResponse.IsFailure)
        {
            return user;
        }

        logger.LogError("Authorization with LDAP failed, user={Username}, code={Code}, message={Message}",
            user.UserName, loginResponse.Error!.Code, loginResponse.Error!.Message);
        return default;
    }
}