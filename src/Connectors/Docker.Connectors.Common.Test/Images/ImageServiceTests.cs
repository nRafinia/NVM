using Connectors.Docker.Abstractions;
using Connectors.Docker.Images;
using Microsoft.Extensions.Logging;
using Moq;

namespace Docker.Connectors.Common.Test.Images;

public class ImageServiceTests
{
    private readonly Mock<ILogger<ImageService>> _mockLogger;
    private readonly Mock<IConnector> _mockConnector;
    private readonly ImageService _imageService;

    public ImageServiceTests()
    {
        _mockLogger = new Mock<ILogger<ImageService>>();
        _mockConnector = new Mock<IConnector>();
        _imageService = new ImageService(_mockLogger.Object);
    }

    [Fact]
    public async Task GetList_ReturnsImages_WhenConnectorSucceeds()
    {
        //arrange
        var expectedImages = new List<Image>
        {
            new ("1","1","latest",DateTime.Now, 1,1,1), 
            new ("2","2","latest",DateTime.Now, 2,2,2)
        };
        _mockConnector
            .Setup(c => c.GetImages(It.IsAny<bool>()))
            .ReturnsAsync(expectedImages);

        //act
        var actualImages = await _imageService.GetList(_mockConnector.Object);

        //assert
        Assert.Equal(expectedImages, actualImages);
    }

    [Fact]
    public async Task GetList_ThrowsException_WhenConnectorFails()
    {
        _mockConnector.Setup(c => c.GetImages(It.IsAny<bool>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(() => _imageService.GetList(_mockConnector.Object));
    }

}