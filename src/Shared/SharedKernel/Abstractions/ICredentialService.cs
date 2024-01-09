using SharedKernel.Entities;

namespace SharedKernel.Abstractions;

public interface ICredentialService
{
    Task<Result<Credential?>> GetCredentialAsync(IdColumn id);
}