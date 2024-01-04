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
            logger.LogError(e, "Error in update none  credential.");
            return e.ToResult();
        }
    }

    private static Credential Update(Credential source, UpdateCredentialNone request)
    {
        source.RemoveBasic();
        
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