using Dashboard.Application.Credentials.AddCredentials.AddCredentialsBasic;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using JetBrains.Annotations;
using Moq;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Test.Credentials.AddCredentials.AddCredentialsBasic
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
                repo => repo.AddAsync(It.IsAny<Credential>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenAnExceptionOccurs_ShouldReturnFailureResult()
        {
            // Arrange
            var addCredentialRequest = new AddCredentialBasic("Test Name", "Test User", "Test Password");
            _mockCredentialRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Credential>()))
                .Throws(new Exception());

            // Act
            var result = await _handler.Handle(addCredentialRequest, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}