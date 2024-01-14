namespace SharedKernel.Base.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;