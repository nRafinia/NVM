using Docker.Connectors.API.Authentications;
using Docker.Connectors.API.Services;
using Docker.DotNet;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Docker.Connectors.API.Test.Services;

public class ApiConnectorTest
{
    private readonly IConfiguration _configuration;

    public ApiConnectorTest()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<ApiConnectorTest>();

        _configuration = builder.Build();
    }
    
    [Fact]
    public async Task GetImages_Success()
    {
        //arrange
        var dockerClientConfiguration =
            new DockerClientConfiguration(new Uri($"http://{_configuration["serverip"]}:2375"), new AnonymousCredentials());
        var authenticator = new Mock<IApiAuthenticate>();
        authenticator.Setup(a => a.GetCredentials())
            .Returns(dockerClientConfiguration);

        //act
        var connector = new ApiConnector(authenticator.Object);
        var images = await connector.GetImages();

        //assert
        Assert.NotEmpty(images);
    }
    
    [Fact]
    public async Task GetContainers_Success()
    {
        //arrange
        var dockerClientConfiguration =
            new DockerClientConfiguration(new Uri($"http://{_configuration["serverip"]}:2375"), new AnonymousCredentials());
        var authenticator = new Mock<IApiAuthenticate>();
        authenticator.Setup(a => a.GetCredentials())
            .Returns(dockerClientConfiguration);

        //act
        var connector = new ApiConnector(authenticator.Object);
        var images = await connector.GetContainers();

        //assert
        Assert.NotEmpty(images);
    }
}