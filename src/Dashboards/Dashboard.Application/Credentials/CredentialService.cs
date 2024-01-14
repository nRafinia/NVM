using Dashboard.Application.Credentials.Queries.GetCredentialById;
using MediatR;
using SharedKernel.Abstractions;
using SharedKernel.Base.Results;
using SharedKernel.Entities;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Credentials;

public class CredentialService(IMediator mediator) : ICredentialService
{
    public Task<Result<Credential?>> GetCredentialAsync(IdColumn id)
    {
        return mediator.Send(new GetCredentialByIdQuery(id));
    }
}