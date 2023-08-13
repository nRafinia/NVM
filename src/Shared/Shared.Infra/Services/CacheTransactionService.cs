using Shared.Domain.Abstractions.Interfaces;
using StackExchange.Redis;

namespace Shared.Infra.Services;

public class CacheTransactionService : ICacheTransaction
{
    private readonly ITransaction _transaction;

    public CacheTransactionService(ICache redisCache, ITransaction transaction)
    {
        _transaction = transaction;
        Cache = redisCache;
    }

    public ICache Cache { get; }

    public Task<bool> ExecuteAsync() => _transaction.ExecuteAsync();
}