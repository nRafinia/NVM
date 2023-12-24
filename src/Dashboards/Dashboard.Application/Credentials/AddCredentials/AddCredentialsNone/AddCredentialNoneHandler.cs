namespace Dashboard.Application.Credentials.AddCredentials.AddCredentialsNone;

internal class AddCredentialNoneHandler(
    ICredentialRepository repository,
    ILogger<AddCredentialNoneHandler> logger)
    : ICommandHandler<AddCredentialNone>
{
    public async Task<Result> Handle(AddCredentialNone request, CancellationToken cancellationToken)
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
            return e.ToResult();
        }
    }
}