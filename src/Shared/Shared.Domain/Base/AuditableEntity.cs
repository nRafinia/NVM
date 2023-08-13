namespace Shared.Domain.Base;

public abstract class AuditableEntity<T, TIdType> : Entity<T, TIdType>, IAuditableEntity<TIdType>
    where T : AuditableEntity<T, TIdType>
    where TIdType : IEquatable<TIdType>
{
    public DateTime Created { get; set; }
    public TIdType CreatedBy { get; set; }
    public DateTime? LastUpdated { get; set; }
    public TIdType? LastUpdatedBy { get; set; }

    protected AuditableEntity()
    {
    }
}

public abstract class AuditableEntity<T> : AuditableEntity<T, long>
    where T : AuditableEntity<T>
{
    protected AuditableEntity()
    {
    }
}