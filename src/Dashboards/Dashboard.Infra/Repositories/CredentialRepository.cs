using System.Data;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Entities;
using Vault;

namespace Dashboard.Infra.Repositories;

public class CredentialRepository(IVaultManager vault, IDateTime dateTime, ICurrentUser userProvider)
    : BaseVaultRepository<Credential>(vault, dateTime, userProvider), ICredentialRepository
{
    protected override string VaultPath => "credentials";

    public override async Task<Credential> AddAsync(Credential item, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadRecords();

        if (credentials.Any(c => c.Id == item.Id))
        {
            throw new DuplicateNameException("Credential id already exists");
        }

        if (credentials.Any(c => string.Equals(c.Name, item.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new DuplicateNameException("Credential name already exists");
        }

        return await base.AddAsync(item, cancellationToken);
    }

    public override async Task<Credential> UpdateAsync(Credential item, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadRecords();

        var existCredentialName =
            credentials.FirstOrDefault(c => string.Equals(c.Name, item.Name, StringComparison.OrdinalIgnoreCase));
        if (existCredentialName is not null && existCredentialName.Id != item.Id)
        {
            throw new Exception("Credential name already exists");
        }

        return await base.UpdateAsync(item, cancellationToken);
    }

    public async ValueTask<Credential?> GetAsync(IdColumn id, CancellationToken cancellationToken = default)
    {
        return await base.GetAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Credential>> GetAsync(string name)
    {
        return await base.GetAllAsync(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default)
    {
        await base.DeleteAsync(c => c.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        await base.DeleteAsync(c => c.Id == credential.Id, cancellationToken);
    }
}