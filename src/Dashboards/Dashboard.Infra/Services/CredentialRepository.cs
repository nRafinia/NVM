using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Licenses;
using Dashboard.Domain.ValueObjects;
using Vault;

namespace Dashboard.Infra.Services;

public class CredentialRepository : ICredentialRepository
{
    private const string VaultPath = "credentials";
    private readonly IVaultManager _vault;
    private readonly byte[] _key;

    private static List<Credential>? _credentials;

    public CredentialRepository(IVaultManager vault)
    {
        _vault = vault;
        var keyResponse = LicenseManager.GetKey();
        if (keyResponse.IsFailure)
        {
            throw new Exception(keyResponse.Error!.Message);
        }

        _key = keyResponse.Value!;
    }

    public async Task AddAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();

        if (credentials.Any(c => c.Id == credential.Id))
        {
            throw new Exception("Credential already exists");
        }

        if (credentials.Any(c => c.Name == credential.Name))
        {
            throw new Exception("Credential name already exists");
        }

        credentials.Add(credential);
        await SaveCredentials();
    }

    public async Task UpdateAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();

        var existCredentialName= credentials.FirstOrDefault(c => c.Name == credential.Name);
        if (existCredentialName is not null && existCredentialName.Id != credential.Id)
        {
            throw new Exception("Credential name already exists");
        }

        if (credentials.Any(c => c.Id == credential.Id))
        {
            throw new Exception("Credential already exists");
        }
        
        credentials.RemoveAll(c => c.Id == credential.Id);
        
        credentials.Add(credential);
        await SaveCredentials();
    }

    public async Task<Credential?> GetAsync(IdColumn id, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();
        return credentials.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Credential?> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();
        return credentials.FirstOrDefault(c => c.Name == name);
    }

    public async Task<IReadOnlyCollection<Credential>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();
        return credentials.AsReadOnly();
    }

    public async Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default)
    {
        var credentials = await LoadCredentials();
        credentials.RemoveAll(c => c.Id == id);

        await SaveCredentials();
    }

    #region private methods
    
    private async Task<List<Credential>> LoadCredentials()
    {
        if (_credentials is not null)
        {
            return _credentials;
        }

        _credentials = await _vault.Decrypt<List<Credential>>(VaultPath, _key);

        if (_credentials is not null)
        {
            return _credentials;
        }

        _credentials = new List<Credential>();
        await SaveCredentials();

        return _credentials;
    }

    private Task SaveCredentials()
    {
        return _credentials is null
            ? Task.CompletedTask
            : _vault.Encrypt(_credentials, VaultPath, _key);
    }
    
    #endregion

}