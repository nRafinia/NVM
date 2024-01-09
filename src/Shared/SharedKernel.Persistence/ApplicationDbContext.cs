using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedKernel.Abstractions;
using SharedKernel.Base;

namespace SharedKernel.Persistence;

public class ApplicationDbContext(DbContextOptions options, IPublisher publisher, IDateTime dateTime)
    : DbContext(options)
{
    /// <summary>
    /// Saves all of the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = dateTime.Now;

        UpdateAuditableEntities(utcNow);

        await PublishDomainEvents(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    /// <summary>
    /// Updates the entities implementing <see cref="AuditableEntity"/> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<AuditableEntity> entityEntry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(AuditableEntity.Created)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(AuditableEntity.LastUpdated)).CurrentValue = utcNow;
            }
        }
    }

    /// <summary>
    /// Publishes and then clears all the domain events that exist within the current transaction.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var entities = ChangeTracker
            .Entries<Entity>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();

        entities.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        IEnumerable<Task> tasks =
            domainEvents.Select(domainEvent => publisher.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }
}