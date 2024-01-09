using System.Linq.Expressions;

namespace SharedKernel.Abstractions;

public interface IBaseRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> GetAsync(IdColumn id, CancellationToken cancellationToken);

    Task<List<TEntity>> FindByField(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<List<TEntity>> FindByField(Expression<Func<TEntity, bool>> predicate, int index, int size,
        CancellationToken cancellationToken);
}