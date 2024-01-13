using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Commands.AddLDAP;

public class AddLdapCommandHandler(
    ILdapRepository repository,
    ILogger<AddLdapCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddLdapCommand>
{
    public async Task<Result> Handle(AddLdapCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existItem = await repository.IsExistNameAsync(request.Name, cancellationToken);
            if (existItem)
            {
                return Result.Failure(SharedErrors.Duplicate("Name already exists."));
            }

            var user = new LDAP(request.Name, request.Port, request.UserSecure, request.HostName, request.CredentialId,
                request.BaseDn, request.FilterQuery, request.Scope, request.AuthenticationType,
                request.ProtocolVersion);
            await repository.AddAsync(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in add new LDAP.");
            return e.ToResult();
        }
    }
}