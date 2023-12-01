using Docker.Connectors.SSH.Helpers;

namespace Docker.Connectors.SSH.Test.Helpers;

public class NetworkParserTests
{
    private const string RawResultMultiple =
        "{\"CreatedAt\":\"2023-11-28 18:27:38.970389872 +0330 +0330\",\"Driver\":\"bridge\",\"ID\":\"ec229d658c19\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"\",\"Name\":\"bridge\",\"Scope\":\"local\"}\n{\"CreatedAt\":\"2022-12-29 23:41:33.00244105 +0330 +0330\",\"Driver\":\"host\",\"ID\":\"d83269a8ec47\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"\",\"Name\":\"host\",\"Scope\":\"local\"}\n{\"CreatedAt\":\"2023-12-01 18:28:00.393931884 +0330 +0330\",\"Driver\":\"bridge\",\"ID\":\"1ed1270c3350\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"\",\"Name\":\"my-network\",\"Scope\":\"local\"}\n{\"CreatedAt\":\"2022-12-29 23:41:32.958262113 +0330 +0330\",\"Driver\":\"null\",\"ID\":\"936c3308bce8\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"\",\"Name\":\"none\",\"Scope\":\"local\"}\n{\"CreatedAt\":\"2023-12-01 11:05:08.505954863 +0330 +0330\",\"Driver\":\"bridge\",\"ID\":\"3e1687d10884\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"com.docker.compose.network=default,com.docker.compose.project=redis,com.docker.compose.version=2.21.0\",\"Name\":\"redis_default\",\"Scope\":\"local\"}";

    private const string RawResultSingle =
        "{\"CreatedAt\":\"2023-11-28 18:27:38.970389872 +0330 +0330\",\"Driver\":\"bridge\",\"ID\":\"ec229d658c19\",\"IPv6\":\"false\",\"Internal\":\"false\",\"Labels\":\"\",\"Name\":\"bridge\",\"Scope\":\"local\"}\n";
    
    [Fact]
    public void Parse_Return_Multiple_Success()
    {
        //arrange

        //act
        var convertor = NetworksParser.List(RawResultMultiple);

        //assert
        Assert.NotEmpty(convertor);
    }  
    
    [Fact]
    public void Parse_Return_Single_Success()
    {
        //arrange

        //act
        var convertor = NetworksParser.List(RawResultSingle);

        //assert
        Assert.NotEmpty(convertor);
    }     
    
    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange
        const string emptyList = "";

        //act
        var convertor = NetworksParser.List(emptyList);

        //assert
        Assert.Empty(convertor);
    }
}