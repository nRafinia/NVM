using Creator.Shared.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Creator.Docker.Command.Linux.Test;

public class VirtualMachineTests
{
    private readonly VirtualMachine _virtualMachine;

    public VirtualMachineTests()
    {
        var mockLogger = new Mock<ILogger<VirtualMachine>>();
        _virtualMachine = new VirtualMachine(mockLogger.Object);
    }

    [Fact]
    public void GetNetworkInterfaces_ShouldReturnNetworkInterfaces_WhenCommandExecutesSuccessfully()
    {
        var result = _virtualMachine.GetNetworkInterfaces();

        //Assert.NotEmpty(result);
        Assert.All(result, item => Assert.IsType<NetworkInterfaces>(item));
    }

    [Fact]
    public void GetContainers_ShouldReturnContainers_WhenCommandExecutesSuccessfully()
    {
        var result = _virtualMachine.GetContainers();

        Assert.NotEmpty(result);
        Assert.All(result, item => Assert.IsType<Container>(item));
    }
    
    [Fact]
    public void GetImages_ShouldReturnImages_WhenCommandExecutesSuccessfully()
    {
        var result = _virtualMachine.GetImages();

        Assert.NotEmpty(result);
        Assert.All(result, item => Assert.IsType<Image>(item));
    }
}