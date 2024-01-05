using Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Base.Results;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Enums;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dashboard.Application.Test.Credentials.Commands.AddCredentials.AddCredentialsNone
{
    [TestSubject(typeof(AddCredentialNoneHandler))]
    public class AddCredentialNoneHandlerTest
    {
        private readonly Mock<ICredentialRepository> _credentialRepositoryMock;
        private readonly AddCredentialNoneHandler _handler;

        public AddCredentialNoneHandlerTest()
        {
            _credentialRepositoryMock = new Mock<ICredentialRepository>();
            Mock<ILogger<AddCredentialNoneHandler>> mockLogger = new();
            _handler = new AddCredentialNoneHandler(_credentialRepositoryMock.Object, mockLogger.Object);
        }

        [Theory]
        [InlineData("name", "description", true)]
        [InlineData("name", null, true)]
        [InlineData(null, "description", false)]
        public async Task Handle_AddsCredentialAndReturnsSuccessResult(string name, string? description, bool expected)
        {
            // Arrange
            var request = new AddCredentialNone(name, description);
            _credentialRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Credential>()));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Equal(expected, result.IsSuccess);
        }

        [Fact]
        public async Task Handle_WhenExceptionCaught_ReturnsFailureResultWithError()
        {
            // Arrange
            var request = new AddCredentialNone("name");
            _credentialRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Credential>()))
                .Throws(new Exception());

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}