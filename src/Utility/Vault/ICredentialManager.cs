namespace Vault;

public interface ICredentialManager
{
    Task Encrypt<T>(T data, string fileName, byte[] key);
    Task<T> Decrypt<T>(string fileName, byte[] key);
}