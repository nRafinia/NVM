using System.Security.Cryptography;
using Moq;

namespace Vault.Test;

public class CredentialManagerTests
{
    [Fact]
    public async Task Encrypt_ShouldWriteEncryptedDataAndKeyToFile()
    {
        // Arrange
        var mockFile = new Mock<IFileUtility>();
        var credentialManager = new VaultManager(mockFile.Object);
        var data = new Test { Name = "Test", Age = 10 };
        var fileName = "test.dat";

        var key = new byte[AesGcm.TagByteSizes.MaxSize];
        RandomNumberGenerator.Fill(key);
        
        // Act
        await credentialManager.Encrypt(data, fileName, key);

        // Assert
        mockFile.Verify(f => f.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), default), Times.Exactly(1));
    }

    [Fact]
    public async Task Decrypt_ShouldReturnDecryptedData()
    {
        // Arrange
        var fileName = "test.dat";

        var data = Array.Empty<byte>();

        var mockFile = new Mock<IFileUtility>();
        mockFile.Setup(f => f.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
            .Callback((string path, byte[] bytes, CancellationToken cancellationToken) => { data = bytes; });

        mockFile.Setup(f => f.ReadAllBytesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string path, CancellationToken cancellationToken) => data);

        mockFile.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
        
        var credentialManager = new VaultManager(mockFile.Object);
        var targetData = new Test { Name = "Test", Age = 10 };
        
        var key = new byte[AesGcm.TagByteSizes.MaxSize];
        RandomNumberGenerator.Fill(key);

        // Act
        await credentialManager.Encrypt(targetData, fileName, key);
        var result = await credentialManager.Decrypt<Test>(fileName, key);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetData.Name, result.Name);
        Assert.Equal(targetData.Age, result.Age);
    }

    private class Test
    {
        public string Name { get; init; }
        public int Age { get; init; }
    }
}