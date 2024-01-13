using Dashboard.Domain.Entities.Users;
using Mapster;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IUserRepository repository,
    ILogger<UpdateUserCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, User>
{
    public async Task<Result<User?>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure<User>(SharedErrors.ItemNotFound);
            }

            user.ChangeDisplayName(request.DisplayName);
            await repository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return user.Adapt<User>();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update user.");
            return e.ToResult<User>();
        }
    }
}