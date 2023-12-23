using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Base.Commands;
using Dashboard.Domain.Base.Results;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Errors;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsNone;

internal class AddApiCredentialNoneHandler(
    ICredentialRepository repository,
    ILogger<AddApiCredentialNoneHandler> logger)
    : ICommandHandler<AddApiCredentialNone, Result>
{
    public async Task<Result<Result?>> Handle(AddApiCredentialNone request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = Credential.None(request.Name, request.Description);
            await repository.AddAsync(credential, cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add api credential none.");
            return Result.Failure(SharedErrors.InternalError);
        }
    }
}