namespace Shared.Application.Abstractions.Data;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
}