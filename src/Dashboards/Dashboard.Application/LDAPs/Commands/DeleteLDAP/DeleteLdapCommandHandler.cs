using Dashboard.Domain.Entities.LDAPs;
using SharedKernel.Abstractions;
using SharedKernel.Base.Commands;
using SharedKernel.Base.Results;
using SharedKernel.Extensions;

namespace Dashboard.Application.LDAPs.Commands.DeleteLDAP;

public class DeleteLdapCommandHandler(
    ILdapRepository repository,
    ILogger<DeleteLdapCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteLdapCommand>
{
    public async Task<Result> Handle(DeleteLdapCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await repository.DeleteAsync(request.Id, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            // ReSharper disable once LogMessageIsSentenceProblem
            logger.LogError(e, "Error in update LDAP.");
            return e.ToResult<LDAP>();
        }
    }
}