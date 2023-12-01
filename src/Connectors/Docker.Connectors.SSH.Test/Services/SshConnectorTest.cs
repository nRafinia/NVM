using Docker.Connectors.SSH.Authentications;
using Docker.Connectors.SSH.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Renci.SshNet;

namespace Docker.Connectors.SSH.Test.Services;

public class SshConnectorTest
{
    private readonly IConfiguration _configuration;

    public SshConnectorTest()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<SshConnectorTest>();

        _configuration = builder.Build();
    }

    [Fact]
    public async Task GetImages_Success()
    {
        //arrange
        var connectionInfo = new ConnectionInfo(_configuration["serverip"], _configuration["username"],
            new PasswordAuthenticationMethod(_configuration["username"], _configuration["password"]));
        var authenticator = new Mock<ISshAuthenticate>();
        authenticator.Setup(a => a.GetCredentials())
            .Returns(connectionInfo);

        //act
        var connector = new SshConnector(authenticator.Object);
        var images = await connector.GetImages();

        //assert
        Assert.NotEmpty(images);
    }
    
    [Fact]
    public async Task GetContainers_Success()
    {
        //arrange
        var connectionInfo = new ConnectionInfo(_configuration["serverip"], _configuration["username"],
            new PasswordAuthenticationMethod(_configuration["username"], _configuration["password"]));
        var authenticator = new Mock<ISshAuthenticate>();
        authenticator.Setup(a => a.GetCredentials())
            .Returns(connectionInfo);

        //act
        var connector = new SshConnector(authenticator.Object);
        var images = await connector.GetContainers();

        //assert
        Assert.NotEmpty(images);
    }    
    
    [Fact]
    public async Task GetNetworks_Success()
    {
        //arrange
        var connectionInfo = new ConnectionInfo(_configuration["serverip"], _configuration["username"],
            new PasswordAuthenticationMethod(_configuration["username"], _configuration["password"]));
        var authenticator = new Mock<ISshAuthenticate>();
        authenticator.Setup(a => a.GetCredentials())
            .Returns(connectionInfo);

        //act
        var connector = new SshConnector(authenticator.Object);
        var networks = await connector.GetNetworks();

        //assert
        Assert.NotEmpty(networks);
    }
}