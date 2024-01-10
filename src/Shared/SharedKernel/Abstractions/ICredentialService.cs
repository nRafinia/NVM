using SharedKernel.Entities;
using SharedKernel.ValueObjects;

namespace SharedKernel.Abstractions;

public interface ICredentialService
{
    Task<Result<Credential?>> GetCredentialAsync(IdColumn id);
}