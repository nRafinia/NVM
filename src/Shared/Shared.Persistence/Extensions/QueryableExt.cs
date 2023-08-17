using System.Linq.Expressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Shared.Persistence.Extensions;

public static class QueryableExt
{
    
    #region Paging
    
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        => query.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    
    public static IQueryable<T> FirstPage<T>(this IQueryable<T> query, int pageSize)
        => query.Take(pageSize);
    
    public static IQueryable<T> LastPage<T>(this IQueryable<T> query, int pageSize)
        => query.Skip(((query.Count()/pageSize) - 1) * pageSize)
            .Take(pageSize);
    
    public static int CountOfPages<T>(this IQueryable<T> query, int pageSize)
    {
        var total = query.Count();
        return (total / pageSize) + ((total % pageSize) > 0 ? 1 : 0);
    }
    
    #endregion
    
    #region Public Methods

    public static int CountWithNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>>? expression = null)
    {
        using var scope = CreateTransaction();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var toReturn = query.Count();
        scope.Complete();
        return toReturn;
    }

    public static async Task<int> CountWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default, Expression<Func<T, bool>>? expression = null)
    {
        using var scope = CreateTransactionAsync();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var toReturn = await query.CountAsync(cancellationToken);
        scope.Complete();
        return toReturn;
    }

    public static T? FirstOrDefaultWithNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>>? expression = null)
    {
        using var scope = CreateTransaction();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var result = query.FirstOrDefault();
        scope.Complete();
        return result;
    }

    public static T? FindWithNoLock<T>(this DbSet<T> dbSet, object?[]? keyValues)
        where T : class
    {
        using var scope = CreateTransaction();
        var result = dbSet.Find(keyValues);
        scope.Complete();
        return result;
    }

    public static async ValueTask<T?> FindWithNoLockAsync<T>(this DbSet<T> dbSet, object?[]? keyValues,
        CancellationToken cancellationToken)
        where T : class
    {
        using var scope = CreateTransactionAsync();
        var result = await dbSet.FindAsync(keyValues, cancellationToken);
        scope.Complete();
        return result;
    }

    public static async Task<T?> FirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default)
    {
        using var scope = CreateTransactionAsync();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var result = await query.FirstOrDefaultAsync(cancellationToken);
        scope.Complete();
        return result;
    }
    
    public static async Task<T?> LastOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        Expression<Func<T, bool>>? expression, CancellationToken cancellationToken = default)
    {
        using var scope = CreateTransactionAsync();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var result = await query.LastOrDefaultAsync(cancellationToken);
        scope.Complete();
        return result;
    }

    public static Task<T?> FirstOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        return FirstOrDefaultWithNoLockAsync(query, null, cancellationToken);
    }
    
    public static Task<T?> LastOrDefaultWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        return LastOrDefaultWithNoLockAsync(query, null, cancellationToken);
    }

    public static List<T> ToListWithNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>>? expression = null)
    {
        using var scope = CreateTransaction();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var result = query.ToList();
        scope.Complete();

        return result;
    }

    public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken = default, Expression<Func<T, bool>>? expression = null)
    {
        using var scope = CreateTransactionAsync();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        var result = await query.ToListAsync(cancellationToken);
        scope.Complete();

        return result;
    }

    #endregion

    #region Private Methods

    private static TransactionScope CreateTransactionAsync() =>
        new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            },
            TransactionScopeAsyncFlowOption.Enabled);

    private static TransactionScope CreateTransaction() =>
        new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            });

    #endregion
}