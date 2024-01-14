using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Commands.SetPassword;

public class SetPasswordCommandHandler(
    IUserRepository repository,
    ILogger<SetPasswordCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<SetPasswordCommand>
{
    public async Task<Result> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure(SharedErrors.ItemNotFound);
            }

            user.SetPassword(request.Password);
            await repository.UpdateAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in set password.");
            return e.ToResult();
        }
    }
}