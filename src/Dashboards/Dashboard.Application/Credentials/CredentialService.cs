using Dashboard.Application.Credentials.Queries.GetCredentialById;
using Dashboard.Domain.ValueObjects;
using MediatR;
using SharedKernel.Abstractions;
using SharedKernel.Base.Results;
using SharedKernel.Entities;

namespace Dashboard.Application.Credentials;

public class CredentialService(IMediator mediator) : ICredentialService
{
    public Task<Result<Credential?>> GetCredentialAsync(IdColumn id)
    {
        return mediator.Send(new GetCredentialByIdQuery(id));
    }
}