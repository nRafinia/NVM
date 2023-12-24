using System.Data;
using Dashboard.Domain.Abstractions;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using Dashboard.Domain.ValueObjects;
using Vault;

namespace Dashboard.Infra.Services;

public class CredentialRepository(IVaultManager vault, IDateTimeProvider dateTime, IUserProvider userProvider)
    : BaseVaultRepository<Credential>(vault, dateTime, userProvider), ICredentialRepository
{
    protected override string VaultPath => "credentials";

    public override async Task<Credential> AddAsync(Credential item)
    {
        var credentials = await LoadRecords();

        if (credentials.Any(c => c.Id == item.Id))
        {
            throw new DuplicateNameException("Credential id already exists");
        }

        if (credentials.Any(c => c.Name == item.Name))
        {
            throw new DuplicateNameException("Credential name already exists");
        }
        
        return await base.AddAsync(item);
    }

    public override async Task<Credential> UpdateAsync(Credential item)
    {
        var credentials = await LoadRecords();

        var existCredentialName = credentials.FirstOrDefault(c => c.Name == item.Name);
        if (existCredentialName is not null && existCredentialName.Id != item.Id)
        {
            throw new Exception("Credential name already exists");
        }

        return await base.UpdateAsync(item);
    }

    public async Task<Credential?> GetAsync(IdColumn id)
    {
        return await base.GetAsync(c => c.Id == id);
    }

    public async Task<Credential?> GetAsync(string name)
    {
        return await base.GetAsync(c => c.Name == name);
    }

    public async Task DeleteAsync(IdColumn id)
    {
        await base.DeleteAsync(c => c.Id == id);
    }

    
}