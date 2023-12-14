using Docker.Connectors.API.Helpers;
using Docker.DotNet.Models;

namespace Docker.Connectors.API.Test.Helpers;

public class ContainersListParserTests
{
    [Fact]
    public void Parse_Return_Multiple_Success()
    {
        //arrange
        var containerData = new List<ContainerListResponse>()
        {
            new ContainerListResponse()
            {
                ID = "5cd808b82e0e732f53a44e7d3b92e68e5dd502cba10540ea6ff49f7d7a5c000b",
                Created = DateTime.Now,
                Image = "redis:latest",
                Labels = new Dictionary<string, string>()
                {
                    {
                        "com.docker.compose.config-hash",
                        "d63b91d8bd0a08703bad6840822c844a44a01110bf6ea70a8f73f211f63b3242"
                    }
                },
                Names = new List<string>()
                {
                    "/redis"
                },
                NetworkSettings = new SummaryNetworkSettings()
                {
                    Networks = new Dictionary<string, EndpointSettings>()
                    {
                        { "redis_default", new EndpointSettings() }
                    }
                },
                Ports = new List<Port>()
                {
                    new Port()
                    {
                        IP = "0.0.0.0",
                        PrivatePort = 6379,
                        PublicPort = 6379,
                        Type = "tcp"
                    }
                },
                State = "running",
                Status = "Up 3 seconds"
            }
        };
        
        //act
        var convertor = ContainersParser.List(containerData);

        //assert
        Assert.NotEmpty(convertor);
        Assert.Equal("redis:latest", convertor[0].Image);
        Assert.NotEmpty(convertor[0].Labels);
        Assert.NotEmpty(convertor[0].Ports);
        Assert.Equal("redis_default", convertor[0].Networks[0]);
    }    
    
    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange

        //act
        var convertor = ContainersParser.List(new List<ContainerListResponse>(0));

        //assert
        Assert.Empty(convertor);
    }
}