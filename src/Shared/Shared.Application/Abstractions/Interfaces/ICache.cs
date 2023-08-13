namespace Shared.Domain.Abstractions.Interfaces;

public interface ICache
{
    Task AddOrUpdateAsync<T>(string key, T item, TimeSpan expireTime);
    Task AddOrUpdateAllAsync<T>(IDictionary<string, T> data, TimeSpan expireTime);
    Task AddSetAsync<T>(string key, string itemKey, T data);
    Task AddSetAsync<T>(string key, string itemKey, T data, TimeSpan expireTime);
    Task AddAllSetAsync<T>(string key, IDictionary<string, T> data);
    Task AddAllSetAsync<T>(string key, IDictionary<string, T> data, TimeSpan expireTime);
    
    Task<T?> GetAsync<T>(string key);
    Task<IDictionary<string, T?>> GetAsync<T>(IEnumerable<string> keys);
    Task<IEnumerable<string>> GetAllSetKeysAsync<T>(string key);
    Task<T?> GetSetAsync<T>(string key, string itemKey);
    Task<IDictionary<string, T?>> GetAllSetAsync<T>(string key);
    Task<IEnumerable<T?>> GetAllSetValuesAsync<T>(string key);
    Task<IDictionary<string, T?>> GetAllSetByKeyAsync<T>(string key, IList<string> itemKeys);

    Task RemoveAsync<T>(string key);
    Task RemoveAllAsync();
    Task RemoveSetAsync(string key, string itemKey);
    Task RemoveAllSetAsync<T>(string key, IEnumerable<string> itemKeys);

    Task<bool> ExistsAsync<T>(string key);
    Task<bool> ExistSetAsync<T>(string key, string itemKey);
    ICacheTransaction CreateTransaction();

}