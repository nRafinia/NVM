using Dashboard.Domain.Enums;

namespace Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

internal class UpdateCredentialBasicHandler(
    ICredentialRepository repository,
    ILogger<UpdateCredentialBasicHandler> logger)
    : ICommandHandler<UpdateCredentialBasic, Credential>
{
    public async Task<Result<Credential?>> Handle(UpdateCredentialBasic request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = await repository.GetAsync(request.Id);
            if (credential is null)
            {
                return Result.Failure<Credential>(SharedErrors.ItemNotFound);
            }

            var updatedCredential = Update(credential, request);

            var newCredential = await repository.UpdateAsync(updatedCredential);
            return newCredential;
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update basic credential.");
            return e.ToResult<Credential>();
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

        if (string.IsNullOrWhiteSpace(request.UserName) && string.IsNullOrWhiteSpace(request.Password))
        {
            return source;
        }

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            if (source.CredentialType == CredentialType.None)
            {
                source.AddBasic(request.UserName, request.Password ?? string.Empty);
                return source;
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                source.BasicCredential!.UpdateUserName(request.UserName);
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            source.BasicCredential!.UpdatePassword(request.Password);
        }

        return source;
    }
}