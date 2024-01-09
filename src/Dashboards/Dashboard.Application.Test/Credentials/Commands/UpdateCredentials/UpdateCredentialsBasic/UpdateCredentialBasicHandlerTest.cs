using Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;
using JetBrains.Annotations;
using Dashboard.Domain.Abstractions.Repositories;
using Moq;
using Dashboard.Domain.Errors;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Enums;
using Dashboard.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using SharedKernel.Errors;

namespace Dashboard.Application.Test.Credentials.Commands.UpdateCredentials.UpdateCredentialsBasic;

[TestSubject(typeof(UpdateCredentialBasicHandler))]
public class UpdateCredentialBasicHandlerTest
{
    private readonly Mock<ICredentialRepository> _mockCredentialRepository;
    private readonly UpdateCredentialBasicHandler _updateCredentialBasicHandler;

    public UpdateCredentialBasicHandlerTest()
    {
        _mockCredentialRepository = new Mock<ICredentialRepository>();
        var mockLogger = new Mock<ILogger<UpdateCredentialBasicHandler>>();
        _updateCredentialBasicHandler =
            new UpdateCredentialBasicHandler(_mockCredentialRepository.Object, mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCredentialNotFound()
    {
        // Arrange
        var request = new UpdateCredentialBasic(IdColumn.New, "Test", "Test", "Test", "Test");
        _mockCredentialRepository
            .Setup(x => x.GetAsync(It.IsAny<IdColumn>()))
            .ReturnsAsync(default(Credential));

        // Act
        var result = await _updateCredentialBasicHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(SharedErrors.ItemNotFound, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCredentialUpdated()
    {
        // Arrange
        var credential = Credential.Basic("Test","UserName","Password");
        var request = new UpdateCredentialBasic(IdColumn.New, "Test1", "UserName1", "Password1", "Description1");

        _mockCredentialRepository
            .Setup(x => x.GetAsync(It.IsAny<IdColumn>()))
            .ReturnsAsync(credential);
        _mockCredentialRepository
            .Setup(x => x.UpdateAsync(It.IsAny<Credential>()))
            .ReturnsAsync(credential);

        // Act
        var result = await _updateCredentialBasicHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        var request = new UpdateCredentialBasic(IdColumn.New, "Test", "Test", "Test", "Test");

        // Act
        _mockCredentialRepository.Setup(x => x.GetAsync(It.IsAny<IdColumn>())).Throws(new Exception());
        var result = await _updateCredentialBasicHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public async Task Handle_ConvertNoneToBasic_Success()
    {
        // Arrange
        var noneCredential = Credential.None("Name");
        var request = new UpdateCredentialBasic(noneCredential.Id, "Name","UserName", "Password");
        
        _mockCredentialRepository
            .Setup(r => r.GetAsync(It.IsAny<IdColumn>()))
            .ReturnsAsync(noneCredential);
        
        // Act
        var result = await _updateCredentialBasicHandler.Handle(request, default);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(CredentialType.Basic, noneCredential.CredentialType);
    }
}