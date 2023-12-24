namespace Dashboard.Domain.Abstractions;

public interface ICurrentUser
{
    IdColumn GetUserId();
}