using Dashboard.Domain.Entities.Users;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.Users.Commands.AddUser;

public class AddUserCommandHandler(
    IUserRepository repository,
    ILogger<AddUserCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddUserCommand>
{
    public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existUser = await repository.IsExistUserNameAsync(request.UserName, cancellationToken);
            if (existUser)
            {
                return Result.Failure(SharedErrors.Duplicate("UserName already exists."));
            }
            
            var user = User.Local(request.UserName, request.Password, request.DisplayName);
            await repository.AddAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add new user.");
            return e.ToResult();
        }
    }
}