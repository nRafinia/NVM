namespace Shared.Domain.Abstractions.Interfaces;

public interface ICacheTransaction 
{
    ICache Cache { get; }
    Task<bool> ExecuteAsync();
}