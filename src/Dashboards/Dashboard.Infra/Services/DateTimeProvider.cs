using Dashboard.Domain.Abstractions;

namespace Dashboard.Infra.Services;

public class DateTimeProvider : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}