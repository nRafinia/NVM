using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Base;

namespace SharedKernel.Persistence.Base;

public abstract class BaseUnitOfWork<T>(T context, IPublisher publisher, IDateTime dateTime, ICurrentUser user)
    : IUnitOfWork
    where T : DbContext
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~BaseUnitOfWork()
    {
        Dispose();
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        var currentDate = dateTime.Now;
        UpdateAuditableEntities(currentDate);
        await PublishDomainEvent(cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(DateTime now)
    {
        foreach (var entityEntry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(AuditableEntity.Created)).CurrentValue = now;
                entityEntry.Property(nameof(AuditableEntity.CreatedBy)).CurrentValue = user.GetUserId();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(AuditableEntity.LastUpdated)).CurrentValue = now;
                entityEntry.Property(nameof(AuditableEntity.LastUpdatedBy)).CurrentValue = user.GetUserId();
            }
        }
    }

    private async Task PublishDomainEvent(CancellationToken cancellationToken)
    {
        var entities = context.ChangeTracker.Entries<Entity>()
            .Where(entryEntity => entryEntity.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities.SelectMany(entry => entry.Entity.DomainEvents).ToList();
        entities.ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents.Select(domainEvent => publisher.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }

}