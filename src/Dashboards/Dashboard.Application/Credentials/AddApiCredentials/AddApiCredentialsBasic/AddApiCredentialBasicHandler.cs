using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Base.Commands;
using Dashboard.Domain.Base.Results;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Errors;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Credentials.AddApiCredentials.AddApiCredentialsBasic;

internal class AddApiCredentialBasicHandler(
    ICredentialRepository repository,
    ILogger<AddApiCredentialBasicHandler> logger)
    : ICommandHandler<AddApiCredentialBasic, Result>
{
    public async Task<Result<Result?>> Handle(AddApiCredentialBasic request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = Credential.Basic(request.Name, request.UserName, request.Password, request.Description);
            await repository.AddAsync(credential, cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add api credential basic.");
            return Result.Failure(SharedErrors.InternalError);
        }
    }
}