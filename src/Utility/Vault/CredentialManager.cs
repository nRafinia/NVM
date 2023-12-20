using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Vault.Models;

namespace Vault;

public class CredentialManager(IFileUtility file) : ICredentialManager
{
    public Task Encrypt<T>(T data, string fileName, byte[] key)
    {
        var json = JsonSerializer.Serialize(data);
        var bytes = Encoding.UTF8.GetBytes(json);

        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        var encryptData = Encrypt(bytes, nonce, key);

        return file.WriteAllBytesAsync(fileName, JsonSerializer.SerializeToUtf8Bytes(encryptData));
    }
    
    public async Task<T> Decrypt<T>(string fileName, byte[] key)
    {
        var encryptData = JsonSerializer.Deserialize<EncryptDataModel>(await file.ReadAllBytesAsync(fileName));

        var json = Decrypt(encryptData, key);

        return JsonSerializer.Deserialize<T>(json)!;
    }
    
    #region Private methods
    
    private static EncryptDataModel Encrypt(byte[] plainTextBytes, byte[] nonce, byte[] key)
    {
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        using var aes = new AesGcm(key, tag.Length);
        var ciphertext = new byte[plainTextBytes.Length];
        aes.Encrypt(nonce, plainTextBytes, ciphertext, tag);

        return new EncryptDataModel(ciphertext, nonce, tag);
    }
    
    private static string Decrypt(EncryptDataModel data, byte[] key)
    {
        using var aes = new AesGcm(key, data.Tag.Length);
        var plaintextBytes = new byte[data.Ciphertext.Length];

        aes.Decrypt(data.Nonce, data.Ciphertext, data.Tag, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
    
    #endregion
}