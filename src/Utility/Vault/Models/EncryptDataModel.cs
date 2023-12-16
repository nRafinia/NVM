namespace Vault.Models;

internal record struct EncryptDataModel(byte[] Ciphertext, byte[] Nonce, byte[] Tag);