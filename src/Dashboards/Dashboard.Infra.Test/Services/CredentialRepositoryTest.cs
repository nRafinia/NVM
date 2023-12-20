using Dashboard.Domain.Entities;
using Moq;
using Vault;
using Dashboard.Domain.Enums;
using Dashboard.Domain.ValueObjects;
using Dashboard.Infra.Services;

namespace Dashboard.Infra.Test.Services;

public class CredentialRepositoryTest
{
    private readonly Mock<IVaultManager> _mockVaultManager;
    private readonly CredentialRepository _subject;

    public CredentialRepositoryTest()
    {
        _mockVaultManager = new Mock<IVaultManager>();
        _subject = new CredentialRepository(_mockVaultManager.Object);
    }

    [Fact]
    public async Task AddAsync_AddsCredential()
    {
        //Arrange
        var credential = new Credential("Name", CredentialType.None);

        //Act
        await _subject.AddAsync(credential);

        //Assert
        _mockVaultManager.Verify(v => v.Encrypt(It.IsAny<List<Credential>>(), It.IsAny<string>(), It.IsAny<byte[]>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesCredential()
    {
        //Arrange
        var credential = new Credential("Name", CredentialType.None);

        //Act
        await _subject.UpdateAsync(credential);

        //Assert
        _mockVaultManager.Verify(v => v.Encrypt(It.IsAny<List<Credential>>(), It.IsAny<string>(), It.IsAny<byte[]>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_GetsCredentialById()
    {
        //Arrange
        var id = IdColumn.New;

        //Act
        var result = await _subject.GetAsync(id);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAsync_GetsCredentialByName()
    {
        //Arrange
        var name = "Name";

        //Act
        var result = await _subject.GetAsync(name);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllAsync_GetsAllCredentials()
    {
        //Act
        var result = await _subject.GetAllAsync();

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_DeletesCredential()
    {
        //Arrange
        var id = IdColumn.New;

        //Act
        await _subject.DeleteAsync(id);

        //Assert
        _mockVaultManager.Verify(v => v.Encrypt(It.IsAny<List<Credential>>(), It.IsAny<string>(), It.IsAny<byte[]>()),
            Times.Once);
    }
}