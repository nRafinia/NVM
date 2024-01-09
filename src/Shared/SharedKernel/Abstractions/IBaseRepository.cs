namespace SharedKernel.Abstractions;

public interface IBaseRepository<TEntity>
    where TEntity : Entity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(IdColumn id, CancellationToken cancellationToken = default);

    ValueTask<TEntity?> GetAsync(IdColumn id, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> GetAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(Func<TEntity, bool> predicate, int index, int size,
        CancellationToken cancellationToken = default);
}