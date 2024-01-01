namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;

internal class UpdateCredentialNoneHandler(
    ICredentialRepository repository,
    ILogger<UpdateCredentialNoneHandler> logger)
    : ICommandHandler<UpdateCredentialNone>
{
    public async Task<Result> Handle(UpdateCredentialNone request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = await repository.GetAsync(request.Id);
            if (credential is null)
            {
                return Result.Failure(SharedErrors.ItemNotFound);
            }

            var updatedCredential = Update(credential, request);
            
            await repository.UpdateAsync(updatedCredential);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add api credential none.");
            return e.ToResult();
        }
    }

    private Credential Update(Credential source, UpdateCredentialNone request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            source.UpdateName(request.Name);
            return source;
        }
        
        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            source.UpdateDescription(request.Description);
        }
        
        return source;
    }
}