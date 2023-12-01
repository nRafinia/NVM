using Docker.Connectors.SSH.Helpers;

namespace Docker.Connectors.SSH.Test.Helpers;

public class ImagesListParserTests
{
    private const string RawResultMultiple =
        "{\"Containers\":\"N/A\",\"CreatedAt\":\"2023-11-10 18:53:15 +0330 +0330\",\"CreatedSince\":\"2 weeks ago\",\"Digest\":\"<none>\",\"ID\":\"961dda256baa\",\"Repository\":\"redis\",\"SharedSize\":\"N/A\",\"Size\":\"138MB\",\"Tag\":\"latest\",\"UniqueSize\":\"N/A\",\"VirtualSize\":\"137.8MB\"}\n{\"Containers\":\"N/A\",\"CreatedAt\":\"2023-08-24 11:01:37 +0330 +0330\",\"CreatedSince\":\"3 months ago\",\"Digest\":\"<none>\",\"ID\":\"2552cbe87d6d\",\"Repository\":\"emby/embyserver\",\"SharedSize\":\"N/A\",\"Size\":\"571MB\",\"Tag\":\"latest\",\"UniqueSize\":\"N/A\",\"VirtualSize\":\"571.4MB\"}";
    private const string RawResultSingle =
        "{\"Containers\":\"N/A\",\"CreatedAt\":\"2023-11-10 18:53:15 +0330 +0330\",\"CreatedSince\":\"2 weeks ago\",\"Digest\":\"<none>\",\"ID\":\"961dda256baa\",\"Repository\":\"redis\",\"SharedSize\":\"N/A\",\"Size\":\"138MB\",\"Tag\":\"latest\",\"UniqueSize\":\"N/A\",\"VirtualSize\":\"137.8MB\"}";

    [Fact]
    public void Parse_Return_Multiple_Success()
    {
        //arrange

        //act
        var convertor = ImagesParser.List(RawResultMultiple);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 2);
        Assert.Equal("redis", convertor[0].Repository);
        Assert.Equal(10, convertor[0].CreatedAt.Day);
        Assert.Equal(144703488, convertor[0].Size);
        Assert.Equal(-1, convertor[0].SharedSize);
    }    
    
    [Fact]
    public void Parse_Return_Single_Success()
    {
        //arrange

        //act
        var convertor = ImagesParser.List(RawResultSingle);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 1);
        Assert.Equal("redis", convertor[0].Repository);
        Assert.Equal(10, convertor[0].CreatedAt.Day);
        Assert.Equal(144703488, convertor[0].Size);
        Assert.Equal(-1, convertor[0].SharedSize);
    }

    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange
        const string emptyList = "";

        //act
        var convertor = ImagesParser.List(emptyList);

        //assert
        Assert.Empty(convertor);
    }
}