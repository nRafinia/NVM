

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsBasic;

internal class AddCredentialBasicHandler(
    ICredentialRepository repository,
    ILogger<AddCredentialBasicHandler> logger)
    : ICommandHandler<AddCredentialBasic>
{
    public async Task<Result> Handle(AddCredentialBasic request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = Credential.Basic(request.Name, request.UserName, request.Password, request.Description);
            await repository.AddAsync(credential);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add api credential basic.");
            return e.ToResult();
        }
    }
}