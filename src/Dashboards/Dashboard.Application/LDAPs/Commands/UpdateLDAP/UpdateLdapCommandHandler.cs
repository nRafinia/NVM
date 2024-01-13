using Dashboard.Domain.Entities.LDAPs;
using Mapster;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Commands.UpdateLDAP;

public class UpdateLdapCommandHandler(
    ILdapRepository repository,
    ILogger<UpdateLdapCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateLdapCommand, LDAP>
{
    public async Task<Result<LDAP?>> Handle(UpdateLdapCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ldap = await repository.GetAsync(request.Id, cancellationToken);

            if (ldap is null)
            {
                return Result.Failure<LDAP>(SharedErrors.ItemNotFound);
            }

            ldap.Update(
                request.Name,
                request.Port,
                request.UserSecure,
                request.HostName,
                request.CredentialId,
                request.BaseDn,
                request.FilterQuery,
                request.Scope,
                request.AuthenticationType,
                request.ProtocolVersion
                );
            await repository.UpdateAsync(ldap, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return ldap;
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update LDAP.");
            return e.ToResult<LDAP>();
        }
    }
}