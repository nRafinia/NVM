namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

internal class UpdateCredentialBasicHandler(
    ICredentialRepository repository,
    ILogger<UpdateCredentialBasicHandler> logger)
    : ICommandHandler<UpdateCredentialBasic>
{
    public async Task<Result> Handle(UpdateCredentialBasic request, CancellationToken cancellationToken)
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
            logger.LogError(e, "Error in update basic credential.");
            return e.ToResult();
        }
    }

    private Credential Update(Credential source, UpdateCredentialBasic request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            source.UpdateName(request.Name);
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            source.UpdateDescription(request.Description);
        }

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            source.BasicCredential!.UpdateUserName(request.UserName);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            source.BasicCredential!.UpdateUserName(request.Password);
        }

        return source;
    }
}