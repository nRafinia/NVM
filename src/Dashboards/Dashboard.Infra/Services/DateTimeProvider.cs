using SharedKernel.Abstractions;

namespace Dashboard.Infra.Services;

public class DateTimeProvider : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}