using Dashboard.Application.Credentials.Commands.AddCredentials.AddCredentialsNone;
using Dashboard.Application.Credentials.Commands.DeleteCredentials;
using Dashboard.Domain.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Test.Credentials.Commands.DeleteCredentials
{
    public class DeleteCredentialCommandHandlerTest
    {
        private readonly Mock<ICredentialRepository> _repositoryMock;
        private readonly DeleteCredentialCommandHandler _deleteCredentialCommandHandler;

        public DeleteCredentialCommandHandlerTest()
        {
            _repositoryMock = new Mock<ICredentialRepository>();
            var logger = new Mock<ILogger<DeleteCredentialCommandHandler>>();
            _deleteCredentialCommandHandler = new DeleteCredentialCommandHandler(_repositoryMock.Object, logger.Object);
        }

        [Fact]
        public async void Handle_Should_Succeed_When_Credentials_Exist()
        {
            // Arrange
            var addCredentialCommand = new AddCredentialNone($"test_{new Random().Next(1000, 9999)}");
            var logger = new Mock<ILogger<AddCredentialNoneHandler>>();
            var addCredentialCommandHandler = new AddCredentialNoneHandler(_repositoryMock.Object, logger.Object);
            

            // Act
            var credential=await addCredentialCommandHandler.Handle(addCredentialCommand, CancellationToken.None);
            var deleteCredentialCommand = new DeleteCredentialCommand(credential.Value!.Id);
            var result = await _deleteCredentialCommandHandler.Handle(deleteCredentialCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async void Handle_Should_Fail_When_Exception_Is_Thrown()
        {
            // Arrange
            var credentialId = new IdColumn(Guid.NewGuid());
            var deleteCredentialCommand = new DeleteCredentialCommand(credentialId);

            _repositoryMock.Setup(o => o.DeleteAsync(credentialId,It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            // Act
            var result = await _deleteCredentialCommandHandler.Handle(deleteCredentialCommand, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.IsFailure);
            _repositoryMock.Verify(repository => repository.DeleteAsync(credentialId,It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}