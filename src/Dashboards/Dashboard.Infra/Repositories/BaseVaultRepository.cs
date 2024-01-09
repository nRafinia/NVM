using Dashboard.Domain.Abstractions;
using Dashboard.Domain.Licenses;
using SharedKernel.Base;
using Vault;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Dashboard.Infra.Repositories;

public abstract class BaseVaultRepository<TEntity>
{
    protected virtual string VaultPath => nameof(TEntity);
    private readonly IVaultManager _vault;
    private readonly IDateTime _dateTimeProvider;
    private readonly ICurrentUser _userProvider;
    private readonly byte[] _key;

    private static List<TEntity>? _records;

    protected BaseVaultRepository(IVaultManager vault, IDateTime dateTime, ICurrentUser userProvider)
    {
        _vault = vault;
        _dateTimeProvider = dateTime;
        _userProvider = userProvider;
        var keyResponse = LicenseManager.GetKey();
        if (keyResponse.IsFailure)
        {
            throw new Exception(keyResponse.Error!.Message);
        }

        _key = keyResponse.Value!;
    }

    public virtual async Task<TEntity> AddAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();

        SetAuditableData(item, false);
        records.Add(item);

        return item;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();

        var existCredentialIndex = records.IndexOf(item);
        if (existCredentialIndex == -1)
        {
            throw new Exception("Item not found");
        }

        SetAuditableData(item, true);
        records[existCredentialIndex] = item;

        return item;
    }

    public virtual async Task DeleteAsync(Predicate<TEntity> predicate, CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();
        records.RemoveAll(predicate);
    }

    public virtual async ValueTask<TEntity?> GetAsync(Func<TEntity, bool> predicate,
        CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();
        return records.FirstOrDefault(predicate);
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _records ?? await LoadRecords();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Func<TEntity, bool> predicate, int index,
        int size, CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();
        return records
            .Where(predicate)
            .Skip(size * index)
            .Take(size)
            .ToList();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Func<TEntity, bool> predicate,
        CancellationToken cancellationToken = default)
    {
        var records = _records ?? await LoadRecords();
        return records
            .Where(predicate)
            .ToList();
    }


    public Task SaveChanges()
    {
        return _records is null
            ? Task.CompletedTask
            : _vault.Encrypt(_records, VaultPath, _key);
    }

    #region protected methods

    protected async Task<List<TEntity>> LoadRecords()
    {
        if (_records is not null)
        {
            return _records;
        }

        _records = await _vault.Decrypt<List<TEntity>>(VaultPath, _key);

        if (_records is not null)
        {
            return _records;
        }

        _records = new List<TEntity>();
        await SaveChanges();

        return _records;
    }

    private void SetAuditableData(TEntity item, bool isUpdate)
    {
        if (item is not AuditableEntity auditableItem)
        {
            return;
        }

        if (!isUpdate)
        {
            auditableItem.Created = _dateTimeProvider.Now;
            auditableItem.CreatedBy = _userProvider.GetUserId();
            return;
        }

        auditableItem.LastUpdated = _dateTimeProvider.Now;
        auditableItem.LastUpdatedBy = _userProvider.GetUserId();
    }

    #endregion
}