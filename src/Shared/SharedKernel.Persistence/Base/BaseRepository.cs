using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Base;
using SharedKernel.ValueObjects;

namespace SharedKernel.Persistence.Base;

public abstract class BaseRepository<TDbContext, TEntity>(TDbContext context) : IBaseRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : Entity
{
    protected readonly TDbContext DbContext = context;
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var addRes = await DbSet.AddAsync(entity, cancellationToken);
        return addRes.Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var currentState = DbContext.Entry(entity).State;
        if (currentState == EntityState.Unchanged)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default)
    {
        DbSet
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public ValueTask<TEntity?> GetAsync(IdColumn id, CancellationToken cancellationToken = default)
        => DbSet.FindAsync([id], cancellationToken);

    public async ValueTask<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => DbSet.AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return DbSet.AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int index, int size,
        CancellationToken cancellationToken = default)
    {
        return DbSet.AsNoTracking()
            .Where(predicate)
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbSet.AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }
}