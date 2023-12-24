using Dashboard.Domain.Abstractions;
using Dashboard.Domain.Base;
using Dashboard.Domain.Licenses;
using Vault;

namespace Dashboard.Infra.Services;

public abstract class BaseVaultRepository<T>
{
    protected virtual string VaultPath => nameof(T);
    private readonly IVaultManager _vault;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserProvider _userProvider;
    private readonly byte[] _key;

    private static List<T>? _records;

    protected BaseVaultRepository(IVaultManager vault, IDateTimeProvider dateTime, IUserProvider userProvider)
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

    public virtual async Task<T> AddAsync(T item)
    {
        var records = _records ?? await LoadRecords();

        SetAuditableData(item, false);
        records.Add(item);
        await SaveRecords();

        return item;
    }

    public virtual async Task<T> UpdateAsync(T item)
    {
        var records = _records ?? await LoadRecords();

        var existCredentialIndex = records.IndexOf(item);
        if (existCredentialIndex == -1)
        {
            throw new Exception("Item not found");
        }

        SetAuditableData(item, true);
        records[existCredentialIndex] = item;
        await SaveRecords();

        return item;
    }

    public virtual async Task<T?> GetAsync(Func<T, bool> predicate)
    {
        var records = _records ?? await LoadRecords();
        return records.FirstOrDefault(predicate);
    }

    public virtual async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        var records = _records ?? await LoadRecords();
        return records.AsReadOnly();
    }

    public virtual async Task DeleteAsync(Predicate<T> predicate)
    {
        var records = _records ?? await LoadRecords();
        records.RemoveAll(predicate);

        await SaveRecords();
    }

    #region protected methods

    protected async Task<List<T>> LoadRecords()
    {
        if (_records is not null)
        {
            return _records;
        }

        _records = await _vault.Decrypt<List<T>>(VaultPath, _key);

        if (_records is not null)
        {
            return _records;
        }

        _records = new List<T>();
        await SaveRecords();

        return _records;
    }

    protected Task SaveRecords()
    {
        return _records is null
            ? Task.CompletedTask
            : _vault.Encrypt(_records, VaultPath, _key);
    }

    private void SetAuditableData(T item, bool isUpdate)
    {
        if (item is not AuditableEntity auditableItem)
        {
            return;
        }

        if (!isUpdate)
        {
            auditableItem.Created = _dateTimeProvider.Now;
            auditableItem.CreatedBy = _userProvider.GetCurrentUserId();
            return;
        }

        auditableItem.LastUpdated = _dateTimeProvider.Now;
        auditableItem.LastUpdatedBy = _userProvider.GetCurrentUserId();
    }

    #endregion
}