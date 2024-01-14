using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IUserRepository repository,
    ILogger<ChangePasswordCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure(SharedErrors.ItemNotFound);
            }

            user.ChangePassword(request.OldPassword, request.NewPassword);
            await repository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in change password.");
            return e.ToResult();
        }
    }
}