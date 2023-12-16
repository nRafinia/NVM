namespace Vault;

public class FileUtility : IFileUtility
{
    public Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default) =>
        File.WriteAllBytesAsync(path, bytes, cancellationToken);
    
    public Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default) =>
        File.ReadAllBytesAsync(path, cancellationToken);
    
}