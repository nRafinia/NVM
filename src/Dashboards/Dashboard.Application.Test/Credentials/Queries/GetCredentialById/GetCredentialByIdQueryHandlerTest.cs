using Dashboard.Application.Credentials.Queries.GetCredentialById;
using Dashboard.Domain.Abstractions.Repositories;
using Dashboard.Domain.Entities;
using Dashboard.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

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

            _repositoryMock.Setup(r => r.GetAsync(goodId))
                .Returns(Task.FromResult<Credential?>(existingCredential));

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(goodId), CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFailure()
        {
            var badId = IdColumn.New;

            _repositoryMock.Setup(r => r.GetAsync(badId))
                .Returns(Task.FromResult<Credential?>(null));

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(badId), CancellationToken.None);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ReturnsFailure()
        {
            var exceptionId = new IdColumn("exceptionId");

            _repositoryMock.Setup(r => r.GetAsync(exceptionId))
                .Throws<Exception>();

            var result = await _queryHandler.Handle(new GetCredentialByIdQuery(exceptionId), CancellationToken.None);

            Assert.True(result.IsFailure);
        }
    }
}