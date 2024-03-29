using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Entities;
using SharedKernel.Extensions;

namespace Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;

public class AddCredentialNoneHandler(
    ICredentialRepository repository,
    ILogger<AddCredentialNoneHandler> logger)
    : ICommandHandler<AddCredentialNone, Credential>
{
    public async Task<Result<Credential?>> Handle(AddCredentialNone request, CancellationToken cancellationToken)
    {
        try
        {
            var credential = Credential.None(request.Name, request.Description);
            await repository.AddAsync(credential);
            return credential;
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add none credential.");
            return e.ToResult<Credential>();
        }
    }
}