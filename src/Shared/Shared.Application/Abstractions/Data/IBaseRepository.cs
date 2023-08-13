using System.Linq.Expressions;
using Shared.Domain.Base;

namespace Shared.Application.Abstractions.Data;

public interface IBaseRepository<TEntity, in T>
    where TEntity : IBaseEntity, IEntity<T>
{
    ValueTask<TEntity?> FindById(T id, CancellationToken cancellationToken);

    Task<IList<TEntity>> FindByField(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, long>
    where TEntity : IEntity<long>
{
}