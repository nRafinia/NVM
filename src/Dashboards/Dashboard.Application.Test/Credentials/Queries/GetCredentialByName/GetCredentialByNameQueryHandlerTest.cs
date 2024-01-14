using Dashboard.Application.Credentials.Queries.GetCredentialByName;
using JetBrains.Annotations;
using Moq;
using Microsoft.Extensions.Logging;
using Dashboard.Domain.Abstractions.Repositories;
using SharedKernel.Entities;

namespace Dashboard.Application.Test.Credentials.Queries.GetCredentialByName;

[TestSubject(typeof(GetCredentialByNameQueryHandler))]
public class GetCredentialByNameQueryHandlerTest
{
    private readonly Mock<ICredentialRepository> _credentialRepositoryMock;
    private readonly GetCredentialByNameQueryHandler _queryHandler;

    public GetCredentialByNameQueryHandlerTest()
    {
        _credentialRepositoryMock = new Mock<ICredentialRepository>();
        var loggerMock = new Mock<ILogger<GetCredentialByNameQueryHandler>>();
        _queryHandler = new GetCredentialByNameQueryHandler(_credentialRepositoryMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCredentials_WhenRepositoryReturnsCredentials()
    {
        // Arrange
        var query = new GetCredentialByNameQuery("test");
        _credentialRepositoryMock.Setup(r => r.GetAsync(query.Name))
            .ReturnsAsync(new List<Credential>());

        // Act
        var result = await _queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        _credentialRepositoryMock.Verify(r => r.GetAsync(query.Name), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenRepositoryThrowsException()
    {
        // Arrange
        var query = new GetCredentialByNameQuery("test");
        _credentialRepositoryMock.Setup(r => r.GetAsync(query.Name)).ThrowsAsync(new Exception());

        // Act
        var result = await _queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        _credentialRepositoryMock.Verify(r => r.GetAsync(query.Name), Times.Once);
    }
}