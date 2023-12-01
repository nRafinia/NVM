using Docker.Connectors.API.Helpers;
using Docker.DotNet.Models;

namespace Docker.Connectors.API.Test.Helpers;

public class NetworksParserTest
{
    [Fact]
    public void Parse_Return_Success()
    {
        //arrange
        var jsonData = new List<NetworkResponse>()
        {
            new()
            {
                ID="ec229d658c19",
                Name="bridge",
                Created=DateTime.Now,
                Scope="local",
                Driver="bridge",
                EnableIPv6=false,
                Internal=false,
                Labels = new Dictionary<string, string>()
                {
                    {"com.docker.compose.network","default"}
                }
            }
        };

        //act
        var convertor = NetworksParser.List(jsonData!);

        //assert
        Assert.NotEmpty(convertor);
        Assert.NotEmpty(convertor.First().Labels);
    }
}