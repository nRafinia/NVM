using System.Text.Json.Serialization;

namespace Vault.Models;

internal class EncryptDataModel(
    byte[] cipherText,
    byte[] nonce,
    byte[] tag)
{
    [JsonPropertyName("c")]
    public byte[] Ciphertext { get; set; } = cipherText;
    
    [JsonPropertyName("n")]
    public byte[] Nonce { get; set; } = nonce;
    
    [JsonPropertyName("t")]
    public byte[] Tag { get; set; } = tag;

}