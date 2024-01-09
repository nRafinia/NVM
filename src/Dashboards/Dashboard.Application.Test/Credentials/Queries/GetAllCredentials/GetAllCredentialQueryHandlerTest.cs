using Dashboard.Application.Credentials.Queries.GetAllCredentials;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using Moq;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Test.Credentials.Queries.GetAllCredentials;

[TestSubject(typeof(GetAllCredentialQueryHandler))]
public class GetAllCredentialQueryHandlerTest
{
    private readonly Mock<ICredentialRepository> _mockRepo;
    private readonly Mock<ILogger<GetAllCredentialQueryHandler>> _mockLogger;

    public GetAllCredentialQueryHandlerTest()
    {
        _mockRepo = new Mock<ICredentialRepository>();
        _mockLogger = new Mock<ILogger<GetAllCredentialQueryHandler>>();
    }

    [Fact]
    public async Task Handle_SuccessfulRepoCall_ShouldReturnSuccessResult()
    {
        var expectedResult = new List<Credential>
        {
            Credential.None("Item1"),
            Credential.None("Item2"),
            Credential.Basic("Item3", "username", "password"),
        };
        _mockRepo
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var handler = new GetAllCredentialQueryHandler(_mockRepo.Object, _mockLogger.Object);

        var result = await handler.Handle(new GetAllCredentialQuery(), new CancellationToken());

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult, result.Value);
    }

    [Fact]
    public async Task Handle_FailedRepoCall_ShouldReturnFailedResult()
    {
        var expectedException = new Exception();
        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ThrowsAsync(expectedException);

        var handler = new GetAllCredentialQueryHandler(_mockRepo.Object, _mockLogger.Object);

        var result = await handler.Handle(new GetAllCredentialQuery(), new CancellationToken());

        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
    }
}