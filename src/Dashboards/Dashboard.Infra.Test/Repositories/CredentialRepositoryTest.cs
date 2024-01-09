using Dashboard.Domain.Abstractions;
using Dashboard.Domain.Licenses;
using Dashboard.Domain.ValueObjects;
using Dashboard.Infra.Repositories;
using Moq;
using SharedKernel.Abstractions;
using SharedKernel.Entities;
using Vault;

namespace Dashboard.Infra.Test.Repositories;

public class CredentialRepositoryTest
{
    private readonly CredentialRepository _subject;

    public CredentialRepositoryTest()
    {
        AsyncHelpers.RunSync(LicenseManager.Load);
        var mockVaultManager = new Mock<IVaultManager>();

        var dateTime = new Mock<IDateTime>();
        dateTime.Setup(d => d.Now).Returns(DateTime.Now);

        var userManager = new Mock<ICurrentUser>();
        userManager.Setup(u => u.GetUserId()).Returns(IdColumn.New);

        _subject = new CredentialRepository(mockVaultManager.Object, dateTime.Object, userManager.Object);
    }

    [Fact]
    public async Task AddAsync_AddsCredential()
    {
        //Arrange
        var name = $"Name-{new Random().Next(1000)}";
        var credential = Credential.None(name);

        //Act
        await _subject.AddAsync(credential);
        var getCredential = await _subject.GetAsync(credential.Id);

        //Assert
        Assert.NotNull(getCredential);
        Assert.Equal(credential.Name, getCredential.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesCredential()
    {
        //Arrange
        var name = $"Name-{new Random().Next(1000)}";
        var name2 = $"Name-{new Random().Next(1000)}";
        var credential = Credential.None(name);

        //Act
        await _subject.AddAsync(credential);
        var getCredential = await _subject.GetAsync(credential.Id);
        getCredential!.UpdateName(name2);
        await _subject.UpdateAsync(getCredential);
        var getCredential2 = await _subject.GetAsync(getCredential.Id);

        //Assert
        Assert.NotNull(getCredential2);
        Assert.Equal(name2, getCredential2.Name);
    }

    [Fact]
    public async Task GetAsync_GetsCredentialById()
    {
        //Arrange
        var name = $"Name-{new Random().Next(1000)}";
        var credential = Credential.None(name);

        //Act
        await _subject.AddAsync(credential);
        var result = await _subject.GetAsync(credential.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(credential, result);
    }

    [Fact]
    public async Task GetAsync_GetsCredentialByName()
    {
        //Arrange
        var name = $"Name-{new Random().Next(1000)}";
        var credential = Credential.None(name);

        //Act
        await _subject.AddAsync(credential);
        var result = await _subject.GetAsync(credential.Name);

        //Assert
        Assert.NotEmpty(result);
    }    
    
    [Fact]
    public async Task GetAsync_GetsCredentialByNameLike()
    {
        //Arrange
        var credential1 = Credential.None($"GetName-{new Random().Next(1000)}");
        var credential2 = Credential.None($"Test-{new Random().Next(1000)}");
        var credential3 = Credential.None($"GetName-{new Random().Next(1000)}");

        //Act
        await _subject.AddAsync(credential1);
        await _subject.AddAsync(credential2);
        await _subject.AddAsync(credential3);
        var result = await _subject.GetAsync("Getname");

        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_GetsAllCredentials()
    {
        //arrange
        var name1 = $"Name-{new Random().Next(1000)}";
        var name2 = $"Name-{new Random().Next(1000)}";
        var credential1 = Credential.None(name1);
        var credential2 = Credential.None(name2);

        //Act
        await _subject.AddAsync(credential1);
        await _subject.AddAsync(credential2);
        var result = await _subject.GetAllAsync();

        //Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task DeleteAsync_DeletesCredential()
    {
        //Arrange
        var name = $"Name-{new Random().Next(1000)}";
        var credential = Credential.None(name);

        //Act
        await _subject.AddAsync(credential);
        await _subject.DeleteAsync(credential.Id);
        var result = await _subject.GetAllAsync();

        //Assert
        Assert.DoesNotContain(result, a => a.Name == name);
    }
}