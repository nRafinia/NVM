using Dashboard.Domain.ValueObjects;

namespace Dashboard.Domain.Base;

public class AuditableEntity(IdColumn id) : Entity(id)
{
    public DateTime Created { get; set; }
    public IdColumn? CreatedBy { get; set; }
    public DateTime? LastUpdated { get; set; }
    public IdColumn? LastUpdatedBy { get; set; }
}