namespace Dashboard.Domain.Abstractions;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}