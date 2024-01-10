using Authorizer.Common.Abstractions;
using Authorizer.Common.Models;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Base.Results;
using SharedKernel.Errors;

namespace Authorizer.Local.Application.Services;

public class LocalAuthorizer(IServiceProvider provider) : IAuthorizer
{
    private readonly IUserRepository _repository = provider.GetRequiredService<IUserRepository>();

    public async Task<Result<List<UserInfo>?>> GetUsers(CancellationToken cancellationToken = default)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        return users
            .Select(u => u.Adapt<UserInfo>()).ToList();
    }

    public async Task<Result<UserInfo?>> SignIn(string userName, string password,
        CancellationToken cancellationToken = default)

    {
        var user = await _repository.GetByUserNameAsync(userName, cancellationToken);
        if (user is null)
        {
            return Result.Failure<UserInfo>(SharedErrors.ItemNotFound);
        }

        return user.CheckPassword(password)
            ? user.Adapt<UserInfo>()
            : Result.Failure<UserInfo>(SharedErrors.InvalidCredential);
    }
}