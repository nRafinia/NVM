using Dashboard.Application.Credentials.Queries.GetCredentialById;
using Dashboard.Domain.Abstractions.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using SharedKernel.Entities;
using SharedKernel.ValueObjects;

namespace Dashboard.Application.Test.Credentials.Queries.GetCredentialById
{
    public class GetCredentialByIdQueryHandlerTest
    {
        private readonly GetCredentialByIdQueryHandler _queryHandler;
        private readonly Mock<ICredentialRepository> _repositoryMock;

        public GetCredentialByIdQueryHandlerTest()
        {
            _repositoryMock = new Mock<ICredentialRepository>();
            var logger = new Mock<ILogger<GetCredentialByIdQueryHandler>>();
            _queryHandler = new GetCredentialByIdQueryHandler(_repositoryMock.Object,
                logger.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsCredential()
        {
            var existingCredential = Credential.None("Name");
            var goodId = existingCredential.Id;

            _repositoryMock.Setup(r => r.GetAsync(goodId,It.IsAny<CancellationToken>()))
                .Returns(ValueTask.FromResult<Credential?>(existingCredential));

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(goodId), CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFailure()
        {
            var badId = IdColumn.New;

            _repositoryMock.Setup(r => r.GetAsync(badId,It.IsAny<CancellationToken>()))
                .Returns(ValueTask.FromResult<Credential?>(null));

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(badId), CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ReturnsFailure()
        {
            var exceptionId = new IdColumn("exceptionId");

            _repositoryMock.Setup(r => r.GetAsync(exceptionId,It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(exceptionId), CancellationToken.None);

            Assert.True(result.IsFailure);
        }
    }
}