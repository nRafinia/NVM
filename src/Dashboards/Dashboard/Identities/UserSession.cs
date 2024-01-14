using SharedKernel.ValueObjects;

namespace Dashboard.Identities;

public class UserSession
{
    public IdColumn UserId { get; set; } = IdColumn.None;
    public string UserName { get; set; } = string.Empty;
}