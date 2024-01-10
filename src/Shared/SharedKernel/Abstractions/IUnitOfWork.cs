namespace SharedKernel.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
}