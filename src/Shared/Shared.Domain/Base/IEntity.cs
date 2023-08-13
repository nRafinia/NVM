using Shared.Domain.Base.Events;

namespace Shared.Domain.Base;

public interface IBaseEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}

public interface IEntity<out T> : IBaseEntity
{
    T Id { get; }
}
