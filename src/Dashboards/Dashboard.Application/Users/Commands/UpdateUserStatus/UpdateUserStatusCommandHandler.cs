using Dashboard.Domain.Entities.Users;
using Dashboard.Domain.Entities.Users.Enums;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusCommandHandler(
    IUserRepository repository,
    ILogger<UpdateUserStatusCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserStatusCommand>
{
    public async Task<Result> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure(SharedErrors.ItemNotFound);
            }

            if (request.Status == UserStatus.Active)
            {
                user.Enable();    
            }
            else
            {
                user.Disable();
            }

            await repository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update user status.");
            return e.ToResult();
        }
    }
}