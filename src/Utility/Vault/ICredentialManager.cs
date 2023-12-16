namespace Vault;

public interface ICredentialManager
{
    Task Encrypt<T>(T data, string fileName, string keyFileName);
    Task<T> Decrypt<T>(string fileName, string keyFileName);
}