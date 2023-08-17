using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions.Data;
using Shared.Domain.Base;
using Shared.Persistence.Extensions;

namespace Shared.Persistence.Repositories;

public abstract class BaseRepository<TDbContext, TEntity, T> : IBaseRepository<TEntity, T>
    where TDbContext : DbContext
    where TEntity : class, IEntity<T>
    where T : IEquatable<T>
{
    protected readonly TDbContext DbContext;
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    protected BaseRepository(TDbContext context)
    {
        DbContext = context;
    }

    public virtual ValueTask<TEntity?> FindById(T id, CancellationToken cancellationToken) =>
        DbSet.FindWithNoLockAsync(new object?[] { id }, cancellationToken);

    public async Task<IList<TEntity>> FindByField(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking()
            .Where(predicate)
            .ToListWithNoLockAsync(cancellationToken);

    public virtual async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
    {
        var addRes = await DbSet.AddAsync(entity, cancellationToken);
        return addRes.Entity;
    }

    public virtual Task Update(TEntity entity)
    {
        var currentState = DbContext.Entry(entity).State;
        if (currentState == EntityState.Unchanged)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task Remove(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }
}

public abstract class BaseRepository<TDbContext, TEntity> : BaseRepository<TDbContext, TEntity, long>
    where TDbContext : DbContext
    where TEntity : class, IEntity<long>

{
    protected BaseRepository(TDbContext context) : base(context)
    {
    }
}