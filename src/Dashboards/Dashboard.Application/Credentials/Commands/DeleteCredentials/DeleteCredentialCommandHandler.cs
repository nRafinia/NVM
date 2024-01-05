namespace Dashboard.Application.Credentials.Commands.DeleteCredentials;

public class DeleteCredentialCommandHandler(
    ICredentialRepository repository,
    ILogger<DeleteCredentialCommandHandler> logger) : ICommandHandler<DeleteCredentialCommand>
{
    public async Task<Result> Handle(DeleteCredentialCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await repository.DeleteAsync(request.Id);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in delete credential.");
            return e.ToResult();
        }

    }
}