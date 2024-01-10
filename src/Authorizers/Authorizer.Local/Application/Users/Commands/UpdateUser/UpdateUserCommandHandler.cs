using Authorizer.Common.Models;
using Authorizer.Local.Domain.Entities;
using Authorizer.Local.Persistence.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Errors;
using SharedKernel.Extensions;

namespace Authorizer.Local.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IUserRepository repository,
    ILogger<UpdateUserCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, UserInfo>
{
    public async Task<Result<UserInfo?>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserInfo>(SharedErrors.ItemNotFound);
            }

            user.ChangeDisplayName(request.DisplayName);
            await repository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return user.Adapt<UserInfo>();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update user.");
            return e.ToResult<UserInfo>();
        }
    }
}