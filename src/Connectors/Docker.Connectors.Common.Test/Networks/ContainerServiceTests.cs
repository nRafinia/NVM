using Connectors.Docker.Abstractions;
using Connectors.Docker.Networks;
using Microsoft.Extensions.Logging;
using Moq;

namespace Docker.Connectors.Common.Test.Networks;

public class NetworkServiceTests
{
    private readonly Mock<ILogger<NetworkService>> _mockLogger;
    private readonly Mock<IConnector> _mockConnector;
    private readonly NetworkService _networkService;

    public NetworkServiceTests()
    {
        _mockLogger = new Mock<ILogger<NetworkService>>();
        _mockConnector = new Mock<IConnector>();
        _networkService = new NetworkService(_mockLogger.Object);
    }

    [Fact]
    public async Task GetList_ReturnsNetworks_WhenConnectorSucceeds()
    {
        //arrange
        var expectedNetworks = new List<Network>
        {
            new ("1","1","host","local",false,new Dictionary<string, string>(),DateTime.Now, false), 
            new ("2","2","host","local",false,new Dictionary<string, string>(),DateTime.Now, false)
        };
        _mockConnector
            .Setup(c => c.GetNetworks())
            .ReturnsAsync(expectedNetworks);

        //act
        var actualNetworks = await _networkService.GetList(_mockConnector.Object);

        //assert
        Assert.Equal(expectedNetworks, actualNetworks);
    }

    [Fact]
    public async Task GetList_ThrowsException_WhenConnectorFails()
    {
        _mockConnector.Setup(c => c.GetNetworks()).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _networkService.GetList(_mockConnector.Object));
    }

}