using Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsBasic;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dashboard.Application.Test.Credentials.Commands.AddCredentials.AddCredentialsBasic
{
    [TestSubject(typeof(AddCredentialBasicHandler))]
    public class AddCredentialBasicHandlerTest
    {
        private readonly Mock<ICredentialRepository> _mockCredentialRepository;
        private readonly AddCredentialBasicHandler _handler;

        public AddCredentialBasicHandlerTest()
        {
            _mockCredentialRepository = new Mock<ICredentialRepository>();
            Mock<ILogger<AddCredentialBasicHandler>> mockLogger = new();
            _handler = new AddCredentialBasicHandler(_mockCredentialRepository.Object, mockLogger.Object);
        }

        [Fact]
        public async Task Handle_WhenCalledWithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            var addCredentialRequest = new AddCredentialBasic("Test Name", "Test User", "Test Password");

            // Act
            var result = await _handler.Handle(addCredentialRequest, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockCredentialRepository.Verify(
                repo => repo.AddAsync(It.IsAny<Credential>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenAnExceptionOccurs_ShouldReturnFailureResult()
        {
            // Arrange
            var addCredentialRequest = new AddCredentialBasic("Test Name", "Test User", "Test Password");
            _mockCredentialRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Credential>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            // Act
            var result = await _handler.Handle(addCredentialRequest, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}