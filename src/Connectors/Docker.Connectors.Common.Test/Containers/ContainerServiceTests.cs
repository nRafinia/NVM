using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Docker.Connectors.Common.Test.Containers;

public class ContainerServiceTests
{
    private readonly Mock<ILogger<ContainerService>> _mockLogger;
    private readonly Mock<IConnector> _mockConnector;
    private readonly ContainerService _containerService;

    public ContainerServiceTests()
    {
        _mockLogger = new Mock<ILogger<ContainerService>>();
        _mockConnector = new Mock<IConnector>();
        _containerService = new ContainerService(_mockLogger.Object);
    }

    [Fact]
    public async Task GetList_ReturnsContainers_WhenConnectorSucceeds()
    {
        //arrange
        var expectedContainers = new List<Container>
        {
            new ("1",DateTime.Now, "1",new Dictionary<string, string>(),new List<string>(){"1"},new List<string>(){"1"},new List<Port>(),"1","1"), 
            new ("2",DateTime.Now, "2",new Dictionary<string, string>(),new List<string>(){"2"},new List<string>(){"2"},new List<Port>(),"2","2")
        };
        _mockConnector
            .Setup(c => c.GetContainers(It.IsAny<bool>()))
            .ReturnsAsync(expectedContainers);

        //act
        var actualContainers = await _containerService.GetList(_mockConnector.Object);

        //assert
        Assert.Equal(expectedContainers, actualContainers);
    }

    [Fact]
    public async Task GetList_ThrowsException_WhenConnectorFails()
    {
        _mockConnector.Setup(c => c.GetContainers(It.IsAny<bool>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _containerService.GetList(_mockConnector.Object));
    }

}