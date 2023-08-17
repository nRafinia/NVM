using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions.Data;
using Shared.Domain.Base;
using Shared.Domain.Base.Events;

namespace Shared.Persistence.Contracts;

public abstract class BaseUnitOfWork<T> : IUnitOfWork
    where T : DbContext
{
    protected readonly T DbContext;
    protected readonly IPublisher Publisher;

    protected BaseUnitOfWork(T context, IPublisher publisher)
    {
        DbContext = context;
        Publisher = publisher;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~BaseUnitOfWork()
    {
        Dispose();
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        PublishDomainEvent();

        return DbContext.SaveChangesAsync(cancellationToken);
    }

    private void PublishDomainEvent()
    {
        var entities = DbContext.ChangeTracker.Entries<IBaseEntity>()
            .Where(entryEntity => entryEntity.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities.SelectMany(e => e.Entity.DomainEvents).ToList();
        entities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            Publish(domainEvent);
        }
    }

    protected abstract Task Publish(IDomainEvent domainEvent);
}