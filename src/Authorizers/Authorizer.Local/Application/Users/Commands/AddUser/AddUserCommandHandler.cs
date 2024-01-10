using Authorizer.Local.Domain.Entities;
using Authorizer.Local.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Authorizer.Local.Application.Users.Commands.AddUser;

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
            var user = new User(request.UserName, request.Password, request.DisplayName);
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