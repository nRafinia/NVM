using Dashboard.Application.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;
using Moq;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using SharedKernel.Entities;
using SharedKernel.Enums;

namespace Dashboard.Application.Test.Credentials.Commands.UpdateCredentials.UpdateCredentialsNone;

public class UpdateCredentialNoneHandlerTests
{
    private readonly Mock<ICredentialRepository> _repository;
    private readonly UpdateCredentialNoneHandler _handler;
    
    public UpdateCredentialNoneHandlerTests()
    {
        _repository = new Mock<ICredentialRepository>();
        var logger = new Mock<ILogger<UpdateCredentialNoneHandler>>();
        _handler = new UpdateCredentialNoneHandler(_repository.Object, logger.Object);
    }
    
    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var stubCredential = Credential.None("UserName");
        var request = new UpdateCredentialNone(stubCredential.Id, "testName", "testDescription");
        
        _repository
            .Setup(r => r.GetAsync(It.IsAny<IdColumn>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(stubCredential);
        
        // Act
        var result = await _handler.Handle(request, default);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("testName", stubCredential.Name);
    }    
    
    [Fact]
    public async Task Handle_ConvertBasicToNone_Success()
    {
        // Arrange
        var basicCredential = Credential.Basic("UserName","UserName", "Password");
        var request = new UpdateCredentialNone(basicCredential.Id, "testName");
        
        _repository
            .Setup(r => r.GetAsync(It.IsAny<IdColumn>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(basicCredential);
        
        // Act
        var result = await _handler.Handle(request, default);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(CredentialType.None, basicCredential.CredentialType);
    }
}