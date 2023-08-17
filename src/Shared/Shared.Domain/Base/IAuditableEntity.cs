namespace Shared.Domain.Base;

public interface IAuditableEntity<T> : IEntity<T>
{
    DateTime Created { get; set; }
    T? CreatedBy { get; set; }
    DateTime? LastUpdated { get; set; }
    T? LastUpdatedBy { get; set; }
}

public interface IAuditableEntity : IAuditableEntity<long>
{
}