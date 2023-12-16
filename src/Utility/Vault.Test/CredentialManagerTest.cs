using Moq;

namespace Vault.Test;

public class CredentialManagerTests
{
    [Fact]
    public async Task Encrypt_ShouldWriteEncryptedDataAndKeyToFile()
    {
        // Arrange
        var mockFile = new Mock<IFileUtility>();
        var credentialManager = new CredentialManager(mockFile.Object);
        var data = new Test { Name = "Test", Age = 10 };
        var fileName = "test.dat";
        var keyFileName = "key.key";

        // Act
        await credentialManager.Encrypt(data, fileName, keyFileName);

        // Assert
        mockFile.Verify(f => f.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), default), Times.Exactly(2));
    }

    [Fact]
    public async Task Decrypt_ShouldReturnDecryptedData()
    {
        // Arrange
        var fileName = "test.dat";
        var keyFileName = "key.key";

        var data = Array.Empty<byte>();
        var key = Array.Empty<byte>();

        var mockFile = new Mock<IFileUtility>();
        mockFile.Setup(f => f.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
            .Callback((string path, byte[] bytes, CancellationToken cancellationToken) =>
            {
                if (path == fileName)
                {
                    data = bytes;
                }

                if (path == keyFileName)
                {
                    key = bytes;
                }
            });

        mockFile.Setup(f => f.ReadAllBytesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string path, CancellationToken cancellationToken) =>
            {
                if (path == fileName)
                {
                    return data;
                }

                if (path == keyFileName)
                {
                    return key;
                }

                return Array.Empty<byte>();
            });

        var credentialManager = new CredentialManager(mockFile.Object);
        var targetData = new Test { Name = "Test", Age = 10 };

        // Act
        await credentialManager.Encrypt(targetData, fileName, keyFileName);
        var result = await credentialManager.Decrypt<Test>(fileName, keyFileName);

        // Assert
        Assert.Equal(targetData.Name, result.Name);
        Assert.Equal(targetData.Age, result.Age);
    }

    private class Test
    {
        public string Name { get; init; }
        public int Age { get; init; }
    }
}